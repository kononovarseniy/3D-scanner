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
            this.calibrationStateLabel = new System.Windows.Forms.Label();
            this.selectSerialButton = new System.Windows.Forms.Button();
            this.rotateButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.statusLabel = new System.Windows.Forms.Label();
            this.scanButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.directControlBox = new System.Windows.Forms.GroupBox();
            this.laserCheckBox = new System.Windows.Forms.CheckBox();
            this.lightCheckBox = new System.Windows.Forms.CheckBox();
            this.deviceSelectorsPanel = new System.Windows.Forms.Panel();
            this.videoCaptureDeviceSelector1 = new Scan3D.VideoCaptureDeviceSelector();
            ((System.ComponentModel.ISupportInitialize)(this.sourceImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grayscaleImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.directControlBox.SuspendLayout();
            this.deviceSelectorsPanel.SuspendLayout();
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
            // calibrationStateLabel
            // 
            this.calibrationStateLabel.AutoSize = true;
            this.calibrationStateLabel.Location = new System.Drawing.Point(9, 92);
            this.calibrationStateLabel.Name = "calibrationStateLabel";
            this.calibrationStateLabel.Size = new System.Drawing.Size(411, 17);
            this.calibrationStateLabel.TabIndex = 9;
            this.calibrationStateLabel.Text = "Not calibrated. (Right click on the center of platform to calibrate)";
            // 
            // selectSerialButton
            // 
            this.selectSerialButton.Location = new System.Drawing.Point(78, 43);
            this.selectSerialButton.Name = "selectSerialButton";
            this.selectSerialButton.Size = new System.Drawing.Size(75, 23);
            this.selectSerialButton.TabIndex = 15;
            this.selectSerialButton.Text = "Select";
            this.selectSerialButton.UseVisualStyleBackColor = true;
            this.selectSerialButton.Click += new System.EventHandler(this.selectSerialButton_Click);
            // 
            // rotateButton
            // 
            this.rotateButton.Location = new System.Drawing.Point(177, 20);
            this.rotateButton.Name = "rotateButton";
            this.rotateButton.Size = new System.Drawing.Size(75, 23);
            this.rotateButton.TabIndex = 16;
            this.rotateButton.Text = "rotate";
            this.rotateButton.UseVisualStyleBackColor = true;
            this.rotateButton.Click += new System.EventHandler(this.rotateButton_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(6, 21);
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
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(6, 53);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(43, 17);
            this.statusLabel.TabIndex = 18;
            this.statusLabel.Text = "Hello!";
            // 
            // scanButton
            // 
            this.scanButton.Enabled = false;
            this.scanButton.Location = new System.Drawing.Point(470, 10);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(100, 77);
            this.scanButton.TabIndex = 19;
            this.scanButton.Text = "SCAN";
            this.scanButton.UseVisualStyleBackColor = true;
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Camera:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "Serial:";
            // 
            // directControlBox
            // 
            this.directControlBox.Controls.Add(this.laserCheckBox);
            this.directControlBox.Controls.Add(this.lightCheckBox);
            this.directControlBox.Controls.Add(this.numericUpDown1);
            this.directControlBox.Controls.Add(this.rotateButton);
            this.directControlBox.Controls.Add(this.statusLabel);
            this.directControlBox.Enabled = false;
            this.directControlBox.Location = new System.Drawing.Point(576, 11);
            this.directControlBox.Name = "directControlBox";
            this.directControlBox.Size = new System.Drawing.Size(723, 76);
            this.directControlBox.TabIndex = 22;
            this.directControlBox.TabStop = false;
            this.directControlBox.Text = "Direct control";
            // 
            // laserCheckBox
            // 
            this.laserCheckBox.AutoSize = true;
            this.laserCheckBox.Location = new System.Drawing.Point(258, 49);
            this.laserCheckBox.Name = "laserCheckBox";
            this.laserCheckBox.Size = new System.Drawing.Size(61, 21);
            this.laserCheckBox.TabIndex = 20;
            this.laserCheckBox.Text = "laser";
            this.laserCheckBox.UseVisualStyleBackColor = true;
            this.laserCheckBox.CheckedChanged += new System.EventHandler(this.laserCheckBox_CheckedChanged);
            // 
            // lightCheckBox
            // 
            this.lightCheckBox.AutoSize = true;
            this.lightCheckBox.Location = new System.Drawing.Point(258, 22);
            this.lightCheckBox.Name = "lightCheckBox";
            this.lightCheckBox.Size = new System.Drawing.Size(56, 21);
            this.lightCheckBox.TabIndex = 19;
            this.lightCheckBox.Text = "light";
            this.lightCheckBox.UseVisualStyleBackColor = true;
            this.lightCheckBox.CheckedChanged += new System.EventHandler(this.lightCheckBox_CheckedChanged);
            // 
            // deviceSelectorsPanel
            // 
            this.deviceSelectorsPanel.Controls.Add(this.label1);
            this.deviceSelectorsPanel.Controls.Add(this.videoCaptureDeviceSelector1);
            this.deviceSelectorsPanel.Controls.Add(this.selectSerialButton);
            this.deviceSelectorsPanel.Controls.Add(this.label2);
            this.deviceSelectorsPanel.Location = new System.Drawing.Point(12, 12);
            this.deviceSelectorsPanel.Name = "deviceSelectorsPanel";
            this.deviceSelectorsPanel.Padding = new System.Windows.Forms.Padding(8);
            this.deviceSelectorsPanel.Size = new System.Drawing.Size(452, 75);
            this.deviceSelectorsPanel.TabIndex = 23;
            // 
            // videoCaptureDeviceSelector1
            // 
            this.videoCaptureDeviceSelector1.Location = new System.Drawing.Point(78, 7);
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
            this.ClientSize = new System.Drawing.Size(1311, 604);
            this.Controls.Add(this.deviceSelectorsPanel);
            this.Controls.Add(this.directControlBox);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.calibrationStateLabel);
            this.Controls.Add(this.grayscaleImage);
            this.Controls.Add(this.sourceImage);
            this.Name = "MainForm";
            this.Text = "3D-сканнер © 2017 Кононов Арсений";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sourceImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grayscaleImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.directControlBox.ResumeLayout(false);
            this.directControlBox.PerformLayout();
            this.deviceSelectorsPanel.ResumeLayout(false);
            this.deviceSelectorsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private VideoCaptureDeviceSelector videoCaptureDeviceSelector1;
        private System.Windows.Forms.PictureBox sourceImage;
        private System.Windows.Forms.PictureBox grayscaleImage;
        private System.Windows.Forms.Label calibrationStateLabel;
        private System.Windows.Forms.Button selectSerialButton;
        private System.Windows.Forms.Button rotateButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox directControlBox;
        private System.Windows.Forms.CheckBox laserCheckBox;
        private System.Windows.Forms.CheckBox lightCheckBox;
        private System.Windows.Forms.Panel deviceSelectorsPanel;
    }
}

