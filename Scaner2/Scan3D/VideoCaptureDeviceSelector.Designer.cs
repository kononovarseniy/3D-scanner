namespace Scan3D
{
    partial class VideoCaptureDeviceSelector
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.deviceSelectComboBox = new System.Windows.Forms.ComboBox();
            this.selectButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // deviceSelectComboBox
            // 
            this.deviceSelectComboBox.DisplayMember = "Name";
            this.deviceSelectComboBox.FormattingEnabled = true;
            this.deviceSelectComboBox.Location = new System.Drawing.Point(0, 0);
            this.deviceSelectComboBox.Name = "deviceSelectComboBox";
            this.deviceSelectComboBox.Size = new System.Drawing.Size(200, 24);
            this.deviceSelectComboBox.TabIndex = 0;
            this.deviceSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.deviceSelectComboBox_SelectedIndexChanged);
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(287, 0);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(75, 23);
            this.selectButton.TabIndex = 3;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(206, 0);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "Refresh";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // VideoCaptureDeviceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.deviceSelectComboBox);
            this.MaximumSize = new System.Drawing.Size(362, 24);
            this.MinimumSize = new System.Drawing.Size(362, 24);
            this.Name = "VideoCaptureDeviceSelector";
            this.Size = new System.Drawing.Size(362, 24);
            this.Load += new System.EventHandler(this.VideoCaptureDeviceSelector_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox deviceSelectComboBox;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button updateButton;
    }
}
