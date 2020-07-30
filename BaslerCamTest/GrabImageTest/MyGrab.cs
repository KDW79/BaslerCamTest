using System;
using Basler.Pylon;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace GrabImageTest
{
    public static class PylonConstant
    {
        public const int BMP = 0;
        public const int TIFF = 1;
        public const int JPEG = 2;
        public const int PNG = 3;
        public const int RAW = 4;

    }

    public static class MyFunction
    {

        public static Image ImageFromRawBgraArray(byte[] arr, int width, int height, PixelFormat pixelFormat)
        {
            var output = new Bitmap(width, height, pixelFormat);
            var rect = new Rectangle(0, 0, width, height);
            var bmpData = output.LockBits(rect, ImageLockMode.ReadWrite, output.PixelFormat);

            // Row-by-row copy
            var arrRowLength = width * Image.GetPixelFormatSize(output.PixelFormat) / 8;
            var ptr = bmpData.Scan0;
            for (var i = 0; i < height; i++)
            {
                Marshal.Copy(arr, i * arrRowLength, ptr, arrRowLength);
                ptr += bmpData.Stride;
            }

            output.UnlockBits(bmpData);
            return output;
        }

        public static Byte[] BayerToRgb(Byte[] arr, ref Int32 width, ref Int32 height, ref Int32 stride, Boolean greenFirst, Boolean blueRowFirst)
        {
            Int32 actualWidth = width - 1;
            Int32 actualHeight = height - 1;
            Int32 actualStride = actualWidth * 3;
            Byte[] result = new Byte[actualStride * actualHeight];
            for (Int32 y = 0; y < actualHeight; y++)
            {
                Int32 curPtr = y * stride;
                Int32 resPtr = y * actualStride;
                Boolean blueRow = y % 2 == (blueRowFirst ? 0 : 1);
                for (Int32 x = 0; x < actualWidth; x++)
                {
                    // Get correct colour components from sliding window
                    Boolean isGreen = (x + y) % 2 == (greenFirst ? 0 : 1);
                    Byte cornerCol1 = isGreen ? arr[curPtr + 1] : arr[curPtr];
                    Byte cornerCol2 = isGreen ? arr[curPtr + stride] : arr[curPtr + stride + 1];
                    Byte greenCol1 = isGreen ? arr[curPtr] : arr[curPtr + 1];
                    Byte greenCol2 = isGreen ? arr[curPtr + stride + 1] : arr[curPtr + stride];
                    Byte blueCol = blueRow ? cornerCol1 : cornerCol2;
                    Byte redCol = blueRow ? cornerCol2 : cornerCol1;
                    // 24bpp RGB is saved as [B, G, R].
                    // Blue
                    result[resPtr + 0] = blueCol;
                    // Green
                    result[resPtr + 1] = (Byte)((greenCol1 + greenCol2) / 2);
                    // Red
                    result[resPtr + 2] = redCol;
                    curPtr++;
                    resPtr += 3;
                }
            }
            height = actualHeight;
            width = actualWidth;
            stride = actualStride;
            return result;
        }
    }

    public class MyGrab
    {
        PixelDataConverter myPixelDataConverter = new PixelDataConverter();
        public Bitmap myBitmap { get; set; }
        public Image myImage { get; set; }


        public void Run()
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
                            Debug.WriteLine("Orientation {0}", grabResult.Orientation);
                            Debug.WriteLine("PaddingX {0}", grabResult.PaddingX);
                            Debug.WriteLine("PaddingY {0}", grabResult.PaddingY);
                            byte[] buffer = grabResult.PixelData as byte[];
                            Debug.WriteLine("Gray value of first pixel: {0}", buffer[0]);
                            Debug.WriteLine("");

                            // Display the grabbed image.
                            //ImageWindow.DisplayImage(0, grabResult);

                            // 이미지를 하드디스크에 저장
                            ImagePersistence.Save(PylonConstant.BMP, @"D:\test.bmp", grabResult);

                            // IImage 출력
                            int width = grabResult.Width;
                            int height = grabResult.Height;
                            int stride = 0;
                            byte[] bufferBayer2Rgb = MyFunction.BayerToRgb(buffer, ref width, ref height, ref stride, false, false);
                            myImage = MyFunction.ImageFromRawBgraArray(bufferBayer2Rgb, grabResult.Width, grabResult.Height, PixelFormat.Format8bppIndexed);

                            //myImage = MyFunction.ImageFromRawBgraArray(buffer, grabResult.Width, grabResult.Height, PixelFormat.Format8bppIndexed);
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
