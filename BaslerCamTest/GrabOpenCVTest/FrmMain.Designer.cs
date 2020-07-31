namespace GrabOpenCVTest
{
    partial class FrmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGrabImage = new System.Windows.Forms.Button();
            this.picBoxGrabbedImage = new System.Windows.Forms.PictureBox();
            this.picBoxCvBw = new System.Windows.Forms.PictureBox();
            this.buttonBW = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGrabbedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCvBw)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGrabImage
            // 
            this.buttonGrabImage.Location = new System.Drawing.Point(12, 12);
            this.buttonGrabImage.Name = "buttonGrabImage";
            this.buttonGrabImage.Size = new System.Drawing.Size(75, 23);
            this.buttonGrabImage.TabIndex = 0;
            this.buttonGrabImage.Text = "Grab";
            this.buttonGrabImage.UseVisualStyleBackColor = true;
            this.buttonGrabImage.Click += new System.EventHandler(this.buttonGrabImage_Click);
            // 
            // picBoxGrabbedImage
            // 
            this.picBoxGrabbedImage.BackColor = System.Drawing.Color.White;
            this.picBoxGrabbedImage.Location = new System.Drawing.Point(12, 41);
            this.picBoxGrabbedImage.Name = "picBoxGrabbedImage";
            this.picBoxGrabbedImage.Size = new System.Drawing.Size(236, 239);
            this.picBoxGrabbedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxGrabbedImage.TabIndex = 1;
            this.picBoxGrabbedImage.TabStop = false;
            // 
            // picBoxCvBw
            // 
            this.picBoxCvBw.BackColor = System.Drawing.Color.White;
            this.picBoxCvBw.Location = new System.Drawing.Point(254, 41);
            this.picBoxCvBw.Name = "picBoxCvBw";
            this.picBoxCvBw.Size = new System.Drawing.Size(236, 239);
            this.picBoxCvBw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxCvBw.TabIndex = 2;
            this.picBoxCvBw.TabStop = false;
            // 
            // buttonBW
            // 
            this.buttonBW.Location = new System.Drawing.Point(254, 12);
            this.buttonBW.Name = "buttonBW";
            this.buttonBW.Size = new System.Drawing.Size(75, 23);
            this.buttonBW.TabIndex = 3;
            this.buttonBW.Text = "흑백화";
            this.buttonBW.UseVisualStyleBackColor = true;
            this.buttonBW.Click += new System.EventHandler(this.buttonBW_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 654);
            this.Controls.Add(this.buttonBW);
            this.Controls.Add(this.picBoxCvBw);
            this.Controls.Add(this.picBoxGrabbedImage);
            this.Controls.Add(this.buttonGrabImage);
            this.Name = "FrmMain";
            this.Text = "OpenCV_ImgProcessingTest";
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGrabbedImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCvBw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGrabImage;
        private System.Windows.Forms.PictureBox picBoxGrabbedImage;
        private System.Windows.Forms.PictureBox picBoxCvBw;
        private System.Windows.Forms.Button buttonBW;
    }
}

