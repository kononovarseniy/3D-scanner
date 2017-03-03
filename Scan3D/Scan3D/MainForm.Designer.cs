namespace Scan3D
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.sourceImage = new System.Windows.Forms.PictureBox();
            this.grayscaleImage = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.selectSerialButton = new System.Windows.Forms.Button();
            this.rotateButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.scanButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.videoCaptureDeviceSelector1 = new Scan3D.VideoCaptureDeviceSelector();
            ((System.ComponentModel.ISupportInitialize)(this.sourceImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grayscaleImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // sourceImage
            // 
            this.sourceImage.Location = new System.Drawing.Point(12, 112);
            this.sourceImage.Name = "sourceImage";
            this.sourceImage.Size = new System.Drawing.Size(640, 480);
            this.sourceImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sourceImage.TabIndex = 4;
            this.sourceImage.TabStop = false;
            this.sourceImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sourceImage_MouseDown);
            // 
            // grayscaleImage
            // 
            this.grayscaleImage.Location = new System.Drawing.Point(659, 112);
            this.grayscaleImage.Name = "grayscaleImage";
            this.grayscaleImage.Size = new System.Drawing.Size(640, 480);
            this.grayscaleImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.grayscaleImage.TabIndex = 7;
            this.grayscaleImage.TabStop = false;
            this.grayscaleImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sourceImage_MouseDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(656, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "XYZ";
            // 
            // trackBar3
            // 
            this.trackBar3.LargeChange = 1;
            this.trackBar3.Location = new System.Drawing.Point(13, 598);
            this.trackBar3.Maximum = 360;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(1287, 56);
            this.trackBar3.TabIndex = 10;
            this.trackBar3.TickFrequency = 4;
            this.trackBar3.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(656, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "XYZ";
            // 
            // selectSerialButton
            // 
            this.selectSerialButton.Location = new System.Drawing.Point(82, 48);
            this.selectSerialButton.Name = "selectSerialButton";
            this.selectSerialButton.Size = new System.Drawing.Size(75, 23);
            this.selectSerialButton.TabIndex = 15;
            this.selectSerialButton.Text = "Select";
            this.selectSerialButton.UseVisualStyleBackColor = true;
            this.selectSerialButton.Click += new System.EventHandler(this.selectSerialButton_Click);
            // 
            // rotateButton
            // 
            this.rotateButton.Location = new System.Drawing.Point(1058, 7);
            this.rotateButton.Name = "rotateButton";
            this.rotateButton.Size = new System.Drawing.Size(75, 23);
            this.rotateButton.TabIndex = 16;
            this.rotateButton.Text = "rotate";
            this.rotateButton.UseVisualStyleBackColor = true;
            this.rotateButton.Click += new System.EventHandler(this.rotateButton_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(887, 8);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            720,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(165, 22);
            this.numericUpDown1.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(884, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "label3";
            // 
            // scanButton
            // 
            this.scanButton.Location = new System.Drawing.Point(450, 12);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(75, 75);
            this.scanButton.TabIndex = 19;
            this.scanButton.Text = "SCAN";
            this.scanButton.UseVisualStyleBackColor = true;
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Camera:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "Serial:";
            // 
            // videoCaptureDeviceSelector1
            // 
            this.videoCaptureDeviceSelector1.Location = new System.Drawing.Point(82, 12);
            this.videoCaptureDeviceSelector1.MaximumSize = new System.Drawing.Size(362, 24);
            this.videoCaptureDeviceSelector1.MinimumSize = new System.Drawing.Size(362, 24);
            this.videoCaptureDeviceSelector1.Name = "videoCaptureDeviceSelector1";
            this.videoCaptureDeviceSelector1.Size = new System.Drawing.Size(362, 24);
            this.videoCaptureDeviceSelector1.TabIndex = 3;
            this.videoCaptureDeviceSelector1.DeviceSelected += new System.EventHandler<Scan3D.VideoDeviceSelectedEventArgs>(this.videoCaptureDeviceSelector1_DeviceSelected);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 667);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.rotateButton);
            this.Controls.Add(this.selectSerialButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grayscaleImage);
            this.Controls.Add(this.sourceImage);
            this.Controls.Add(this.videoCaptureDeviceSelector1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sourceImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grayscaleImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private VideoCaptureDeviceSelector videoCaptureDeviceSelector1;
        private System.Windows.Forms.PictureBox sourceImage;
        private System.Windows.Forms.PictureBox grayscaleImage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button selectSerialButton;
        private System.Windows.Forms.Button rotateButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

