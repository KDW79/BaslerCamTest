﻿using System;
using System.Windows.Forms;
using MyGrab;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.CPlusPlus;
using System.Diagnostics;

namespace GrabOpenCVTest
{

    public partial class FrmMain : Form
    {
        MyGrabBase myGrabBase = new MyGrabBase();

        enum ImreadModes
        {
            UNCHANGED = -1, //!< If set, return the loaded image as is (with alpha channel, otherwise it gets cropped).
            GRAYSCALE = 0,  //!< If set, always convert image to the single channel grayscale image (codec internal conversion).
            COLOR = 1,  //!< If set, always convert image to the 3 channel BGR color image.
            ANYDEPTH = 2,  //!< If set, return 16-bit/32-bit image when the input has the corresponding depth, otherwise convert it to 8-bit.
            ANYCOLOR = 4,  //!< If set, the image is read in any possible color format.
            LOAD_GDAL = 8,  //!< If set, use the gdal driver for loading the image.
            REDUCED_GRAYSCALE_2 = 16, //!< If set, always convert image to the single channel grayscale image and the image size reduced 1/2.
            REDUCED_COLOR_2 = 17, //!< If set, always convert image to the 3 channel BGR color image and the image size reduced 1/2.
            REDUCED_GRAYSCALE_4 = 32, //!< If set, always convert image to the single channel grayscale image and the image size reduced 1/4.
            REDUCED_COLOR_4 = 33, //!< If set, always convert image to the 3 channel BGR color image and the image size reduced 1/4.
            REDUCED_GRAYSCALE_8 = 64, //!< If set, always convert image to the single channel grayscale image and the image size reduced 1/8.
            REDUCED_COLOR_8 = 65, //!< If set, always convert image to the 3 channel BGR color image and the image size reduced 1/8.
            IGNORE_ORIENTATION = 128 //!< If set, do not rotate the image according to EXIF's orientation flag.
        };

        enum ColorConversionCodes
        {
            BGR2BGRA = 0,
            RGB2RGBA = 0,
            BGRA2BGR = 1,
            RGBA2RGB = 1,
            BGR2RGBA = 2,
            RGB2BGRA = 2,
            RGBA2BGR = 3,
            BGRA2RGB = 3,
            BGR2RGB = 4,
            RGB2BGR = 4,
            BGRA2RGBA = 5,
            RGBA2BGRA = 5,
            BGR2GRAY = 6,
            RGB2GRAY = 7,
            GRAY2BGR = 8,
            GRAY2RGB = 8,
            GRAY2BGRA = 9,
            GRAY2RGBA = 9,
            BGRA2GRAY = 10,
            RGBA2GRAY = 11,
            BGR2BGR565 = 12,
            RGB2BGR565 = 13,
            BGR5652BGR = 14,
            BGR5652RGB = 15,
            BGRA2BGR565 = 16,
            RGBA2BGR565 = 17,
            BGR5652BGRA = 18,
            BGR5652RGBA = 19,
            GRAY2BGR565 = 20,
            BGR5652GRAY = 21,
            BGR2BGR555 = 22,
            RGB2BGR555 = 23,
            BGR5552BGR = 24,
            BGR5552RGB = 25,
            BGRA2BGR555 = 26,
            RGBA2BGR555 = 27,
            BGR5552BGRA = 28,
            BGR5552RGBA = 29,
            GRAY2BGR555 = 30,
            BGR5552GRAY = 31,
            BGR2XYZ = 32,
            RGB2XYZ = 33,
            XYZ2BGR = 34,
            XYZ2RGB = 35,
            BGR2YCrCb = 36,
            RGB2YCrCb = 37,
            YCrCb2BGR = 38,
            YCrCb2RGB = 39,
            BGR2HSV = 40,
            RGB2HSV = 41,
            BGR2Lab = 44,
            RGB2Lab = 45,
            BGR2Luv = 50,
            RGB2Luv = 51,
            BGR2HLS = 52,
            RGB2HLS = 53,
            HSV2BGR = 54,
            HSV2RGB = 55,
            Lab2BGR = 56,
            Lab2RGB = 57,
            Luv2BGR = 58,
            Luv2RGB = 59,
            HLS2BGR = 60,
            HLS2RGB = 61,
            BGR2HSV_FULL = 66,
            RGB2HSV_FULL = 67,
            BGR2HLS_FULL = 68,
            RGB2HLS_FULL = 69,
            HSV2BGR_FULL = 70,
            HSV2RGB_FULL = 71,
            HLS2BGR_FULL = 72,
            HLS2RGB_FULL = 73,
            LBGR2Lab = 74,
            LRGB2Lab = 75,
            LBGR2Luv = 76,
            LRGB2Luv = 77,
            Lab2LBGR = 78,
            Lab2LRGB = 79,
            Luv2LBGR = 80,
            Luv2LRGB = 81,
            BGR2YUV = 82,
            RGB2YUV = 83,
            YUV2BGR = 84,
            YUV2RGB = 85,
            YUV2RGB_NV12 = 90,
            YUV2BGR_NV12 = 91,
            YUV2RGB_NV21 = 92,
            YUV2BGR_NV21 = 93,
            YUV420sp2RGB = 92,
            YUV420sp2BGR = 93,
            YUV2RGBA_NV12 = 94,
            YUV2BGRA_NV12 = 95,
            YUV2RGBA_NV21 = 96,
            YUV2BGRA_NV21 = 97,
            YUV420sp2RGBA = 96,
            YUV420sp2BGRA = 97,
            YUV2RGB_YV12 = 98,
            YUV2BGR_YV12 = 99,
            YUV2RGB_IYUV = 100,
            YUV2BGR_IYUV = 101,
            YUV2RGB_I420 = 100,
            YUV2BGR_I420 = 101,
            YUV420p2RGB = 98,
            YUV420p2BGR = 99,
            YUV2RGBA_YV12 = 102,
            YUV2BGRA_YV12 = 103,
            YUV2RGBA_IYUV = 104,
            YUV2BGRA_IYUV = 105,
            YUV2RGBA_I420 = 104,
            YUV2BGRA_I420 = 105,
            YUV420p2RGBA = 102,
            YUV420p2BGRA = 103,
            YUV2GRAY_420 = 106,
            YUV2GRAY_NV21 = 106,
            YUV2GRAY_NV12 = 106,
            YUV2GRAY_YV12 = 106,
            YUV2GRAY_IYUV = 106,
            YUV2GRAY_I420 = 106,
            YUV420sp2GRAY = 106,
            YUV420p2GRAY = 106,
            YUV2RGB_UYVY = 107,
            YUV2BGR_UYVY = 108,
            YUV2RGB_Y422 = 107,
            YUV2BGR_Y422 = 108,
            YUV2RGB_UYNV = 107,
            YUV2BGR_UYNV = 108,
            YUV2RGBA_UYVY = 111,
            YUV2BGRA_UYVY = 112,
            YUV2RGBA_Y422 = 111,
            YUV2BGRA_Y422 = 112,
            YUV2RGBA_UYNV = 111,
            YUV2BGRA_UYNV = 112,
            YUV2RGB_YUY2 = 115,
            YUV2BGR_YUY2 = 116,
            YUV2RGB_YVYU = 117,
            YUV2BGR_YVYU = 118,
            YUV2RGB_YUYV = 115,
            YUV2BGR_YUYV = 116,
            YUV2RGB_YUNV = 115,
            YUV2BGR_YUNV = 116,
            YUV2RGBA_YUY2 = 119,
            YUV2BGRA_YUY2 = 120,
            YUV2RGBA_YVYU = 121,
            YUV2BGRA_YVYU = 122,
            YUV2RGBA_YUYV = 119,
            YUV2BGRA_YUYV = 120,
            YUV2RGBA_YUNV = 119,
            YUV2BGRA_YUNV = 120,
            YUV2GRAY_UYVY = 123,
            YUV2GRAY_YUY2 = 124,
            YUV2GRAY_Y422 = 123,
            YUV2GRAY_UYNV = 123,
            YUV2GRAY_YVYU = 124,
            YUV2GRAY_YUYV = 124,
            YUV2GRAY_YUNV = 124,
            RGBA2mRGBA = 125,
            mRGBA2RGBA = 126,
            RGB2YUV_I420 = 127,
            BGR2YUV_I420 = 128,
            RGB2YUV_IYUV = 127,
            BGR2YUV_IYUV = 128,
            RGBA2YUV_I420 = 129,
            BGRA2YUV_I420 = 130,
            RGBA2YUV_IYUV = 129,
            BGRA2YUV_IYUV = 130,
            RGB2YUV_YV12 = 131,
            BGR2YUV_YV12 = 132,
            RGBA2YUV_YV12 = 133,
            BGRA2YUV_YV12 = 134,
            BayerBG2BGR = 46,
            BayerGB2BGR = 47,
            BayerRG2BGR = 48,
            BayerGR2BGR = 49,
            BayerBG2RGB = 48,
            BayerGB2RGB = 49,
            BayerRG2RGB = 46,
            BayerGR2RGB = 47,
            BayerBG2GRAY = 86,
            BayerGB2GRAY = 87,
            BayerRG2GRAY = 88,
            BayerGR2GRAY = 89,
            BayerBG2BGR_VNG = 62,
            BayerGB2BGR_VNG = 63,
            BayerRG2BGR_VNG = 64,
            BayerGR2BGR_VNG = 65,
            BayerBG2RGB_VNG = 64,
            BayerGB2RGB_VNG = 65,
            BayerRG2RGB_VNG = 62,
            BayerGR2RGB_VNG = 63,
            BayerBG2BGR_EA = 135,
            BayerGB2BGR_EA = 136,
            BayerRG2BGR_EA = 137,
            BayerGR2BGR_EA = 138,
            BayerBG2RGB_EA = 137,
            BayerGB2RGB_EA = 138,
            BayerRG2RGB_EA = 135,
            BayerGR2RGB_EA = 136,
            COLORCVT_MAX = 139,
        };

        public FrmMain()
        {
            InitializeComponent();
        }

        private void buttonGrabImage_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath;
            myGrabBase.GrabImage(path);
            picBoxGrabbedImage.Image = myGrabBase.myBitmap;
        }


        private void buttonBW_Click(object sender, EventArgs e)
        {
            Mat mySrc = BitmapConverter.ToMat(myGrabBase.myBitmap);
            Mat mySrc2Gray = new Mat();
            Mat mySrc2Binary = new Mat();

            try
            {
                //Mat image = Cv2.ImRead(@"D:\20200731_120705_GrabOrg.BMP", (int)ImreadModes.GRAYSCALE);
                //Cv2.ImShow("image", image);
                Cv2.CvtColor(mySrc, mySrc2Gray, ColorConversion.BgrToGray);
                Cv2.Threshold(mySrc2Gray, mySrc2Binary, 150, 255, ThresholdType.Binary);

                picBoxCvBw.Image = mySrc2Binary.ToBitmap();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.ToString()}");
            }
            finally
            {
                //Cv2.DestroyAllWindows();
            }
        }
    }
}
