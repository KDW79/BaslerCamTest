namespace GrabImageTest
{
    partial class FormMain
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
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGrabbedImage)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGrabImage
            // 
            this.buttonGrabImage.Location = new System.Drawing.Point(12, 24);
            this.buttonGrabImage.Name = "buttonGrabImage";
            this.buttonGrabImage.Size = new System.Drawing.Size(75, 23);
            this.buttonGrabImage.TabIndex = 0;
            this.buttonGrabImage.Text = "Grab";
            this.buttonGrabImage.UseVisualStyleBackColor = true;
            this.buttonGrabImage.Click += new System.EventHandler(this.buttonGrabImage_Click);
            // 
            // picBoxGrabbedImage
            // 
            this.picBoxGrabbedImage.Location = new System.Drawing.Point(12, 53);
            this.picBoxGrabbedImage.Name = "picBoxGrabbedImage";
            this.picBoxGrabbedImage.Size = new System.Drawing.Size(738, 560);
            this.picBoxGrabbedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxGrabbedImage.TabIndex = 1;
            this.picBoxGrabbedImage.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 625);
            this.Controls.Add(this.picBoxGrabbedImage);
            this.Controls.Add(this.buttonGrabImage);
            this.Name = "FormMain";
            this.Text = "GrabImage";
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGrabbedImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGrabImage;
        private System.Windows.Forms.PictureBox picBoxGrabbedImage;
    }
}

