using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basler.Pylon;

namespace BaslerCamTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Camera camera = new Camera())
            {
                camera.Open();

                Console.WriteLine("Camera Device Information");
                Console.WriteLine("=========================");
                Console.WriteLine("Vendor           : {0}", camera.Parameters[PLCamera.DeviceVendorName].GetValue());
                Console.WriteLine("Model            : {0}", camera.Parameters[PLCamera.DeviceModelName].GetValue());
                Console.WriteLine("Firmware version : {0}", camera.Parameters[PLCamera.DeviceFirmwareVersion].GetValue());
                Console.WriteLine("");
                Console.WriteLine("Camera Device Settings");
                Console.WriteLine("======================");


                camera.StreamGrabber.Start();

                for (int i = 0; i < 10; ++i)
                {
                    IGrabResult grabResult = camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                    using (grabResult)
                    {
                        if (grabResult.GrabSucceeded)
                        {
                            Console.WriteLine("SizeX: {0}", grabResult.Width);
                            Console.WriteLine("SizeY: {0}", grabResult.Height);
                            byte[] buffer = grabResult.PixelData as byte[];
                            Console.WriteLine("Gray value of first pixel: {0}", buffer[0]);
                            Console.WriteLine("");
                        }
                        else
                        {
                            Console.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
                        }
                    }
                }

                camera.StreamGrabber.Stop();

                camera.Close();
            }
        }
    }
}
