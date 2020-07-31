using System;
using Basler.Pylon;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace GrabImageTest
{
    public class MyGrab
    {
        enum ImageFileFormat
        {
            BMP, // 0
            TIFF, // 1
            JPEG, // 2
            PNG, // 3
            RAW, // 4 
        }

        PixelDataConverter myPixelDataConverter = new PixelDataConverter();
        public Bitmap myBitmap { get; set; }
        public Image myImage { get; set; }

        public void GrabImage(string path)
        {
            try
            {
                // Create a camera object that selects the first camera device found.
                // More constructors are available for selecting a specific camera device.
                using (Camera camera = new Camera())
                {
                    // Print the model name of the camera.
                    Debug.WriteLine("Using camera {0}.", camera.CameraInfo[CameraInfoKey.ModelName]);

                    // Set the acquisition mode to free running continuous acquisition when the camera is opened.
                    camera.CameraOpened += Configuration.AcquireContinuous;

                    // Open the connection to the camera device.
                    camera.Open();

                    // The parameter MaxNumBuffer can be used to control the amount of buffers
                    // allocated for grabbing. The default value of this parameter is 10.
                    camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);

                    // Start grabbing.
                    camera.StreamGrabber.Start();

                    // Grab a image.
                    // Wait for an image and then retrieve it. A timeout of 5000 ms is used.
                    IGrabResult grabResult = camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);

                    using (grabResult)
                    {
                        // Image grabbed successfully?
                        if (grabResult.GrabSucceeded)
                        {
                            // Access the image data.
                            Debug.WriteLine("SizeX: {0}", grabResult.Width);
                            Debug.WriteLine("SizeY: {0}", grabResult.Height);
                            Debug.WriteLine("IsValid: {0}", grabResult.IsValid);
                            Debug.WriteLine("Orientation: {0}", grabResult.Orientation);
                            Debug.WriteLine("PaddingX: {0}", grabResult.PaddingX);
                            Debug.WriteLine("PaddingY: {0}", grabResult.PaddingY);
                            byte[] buffer = grabResult.PixelData as byte[];
                            Debug.WriteLine("PixelData Count: {0}", buffer.Length);
                            //Debug.WriteLine("Gray value of first pixel: {0}", buffer[0]);
                            Debug.WriteLine("");

                            // Display the grabbed image.
                            //ImageWindow.DisplayImage(0, grabResult);

                            // 이미지를 하드디스크에 저장
                            ImagePersistence.Save((int)ImageFileFormat.BMP, path + "\\" + DateTime.Now.ToString("yyyyMMdd_HHMMss") + "_GrabOrg.bmp", grabResult);
                            //ImagePersistence.Save((int)ImageFileFormat.BMP, @"D:\" + DateTime.Now.ToString("yyyyMMdd_HHMMss") + "_GrabOrg.bmp", grabResult);

                            // IImage를 bitmap 형식으로 변환
                            myBitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                            // Lock the bits of the bitmap.
                            BitmapData bmpData = myBitmap.LockBits(new Rectangle(0, 0, myBitmap.Width, myBitmap.Height), ImageLockMode.ReadWrite, myBitmap.PixelFormat);
                            // Place the pointer to the buffer of the bitmap.
                            myPixelDataConverter.OutputPixelFormat = PixelType.BGRA8packed;
                            IntPtr ptrBmp = bmpData.Scan0;
                            myPixelDataConverter.Convert(ptrBmp, bmpData.Stride * myBitmap.Height, grabResult);
                            myBitmap.UnlockBits(bmpData);

                        }
                        else
                        {
                            Debug.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
                        }
                    }

                    // Stop grabbing.
                    camera.StreamGrabber.Stop();

                    // Close the connection to the camera device.
                    camera.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: {0}", e.Message);
            }
        }
    }

}
