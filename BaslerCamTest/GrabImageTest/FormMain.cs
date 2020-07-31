using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrabImageTest
{
    public partial class FormMain : Form
    {

        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonGrabImage_Click(object sender, EventArgs e)
        {
            MyGrab myGrab = new MyGrab();
            string path = Application.StartupPath;
            myGrab.GrabImage(path);
            //picBoxGrabbedImage.Image = myGrab.myImage;
            picBoxGrabbedImage.Image = myGrab.myBitmap;
        }
    }
}
