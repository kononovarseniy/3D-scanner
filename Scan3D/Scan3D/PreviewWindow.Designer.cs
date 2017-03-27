namespace Scan3D
{
    partial class PreviewWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.resetTransfromButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.largeWidthFaceFilterMaxLength = new System.Windows.Forms.NumericUpDown();
            this.largeWidthFaceFilterRadioButton = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.largeFaceFilterMaxLength = new System.Windows.Forms.NumericUpDown();
            this.largeFaceFilterRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.thinFaceFilterAngle = new System.Windows.Forms.NumericUpDown();
            this.thinFaceFilterRadioButton = new System.Windows.Forms.RadioButton();
            this.noFaceFilterRadioButton = new System.Windows.Forms.RadioButton();
            this.meshRenderer1 = new Scan3D.GraphicsUtils.MeshRenderer();
            this.completeButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.largeWidthFaceFilterMaxLength)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.largeFaceFilterMaxLength)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thinFaceFilterAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // resetTransfromButton
            // 
            this.resetTransfromButton.Location = new System.Drawing.Point(718, 12);
            this.resetTransfromButton.Name = "resetTransfromButton";
            this.resetTransfromButton.Size = new System.Drawing.Size(270, 23);
            this.resetTransfromButton.TabIndex = 1;
            this.resetTransfromButton.Text = "Reset position";
            this.resetTransfromButton.UseVisualStyleBackColor = true;
            this.resetTransfromButton.Click += new System.EventHandler(this.resetTransfromButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(718, 660);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(269, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.largeWidthFaceFilterRadioButton);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.largeFaceFilterRadioButton);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.thinFaceFilterRadioButton);
            this.groupBox1.Controls.Add(this.noFaceFilterRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(719, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 234);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Face filter";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.largeWidthFaceFilterMaxLength);
            this.panel3.Location = new System.Drawing.Point(6, 194);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(257, 28);
            this.panel3.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Max length";
            // 
            // largeWidthFaceFilterMaxLength
            // 
            this.largeWidthFaceFilterMaxLength.DecimalPlaces = 1;
            this.largeWidthFaceFilterMaxLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.largeWidthFaceFilterMaxLength.Location = new System.Drawing.Point(98, 3);
            this.largeWidthFaceFilterMaxLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.largeWidthFaceFilterMaxLength.Name = "largeWidthFaceFilterMaxLength";
            this.largeWidthFaceFilterMaxLength.Size = new System.Drawing.Size(156, 22);
            this.largeWidthFaceFilterMaxLength.TabIndex = 0;
            this.largeWidthFaceFilterMaxLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.largeWidthFaceFilterMaxLength.ValueChanged += new System.EventHandler(this.largeWidthFaceFilter_ParameterChandeg);
            // 
            // largeWidthFaceFilterRadioButton
            // 
            this.largeWidthFaceFilterRadioButton.AutoSize = true;
            this.largeWidthFaceFilterRadioButton.Location = new System.Drawing.Point(6, 167);
            this.largeWidthFaceFilterRadioButton.Name = "largeWidthFaceFilterRadioButton";
            this.largeWidthFaceFilterRadioButton.Size = new System.Drawing.Size(133, 21);
            this.largeWidthFaceFilterRadioButton.TabIndex = 5;
            this.largeWidthFaceFilterRadioButton.TabStop = true;
            this.largeWidthFaceFilterRadioButton.Text = "Large width filter";
            this.largeWidthFaceFilterRadioButton.UseVisualStyleBackColor = true;
            this.largeWidthFaceFilterRadioButton.CheckedChanged += new System.EventHandler(this.largeWidthFaceFilter_ParameterChandeg);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.largeFaceFilterMaxLength);
            this.panel2.Location = new System.Drawing.Point(6, 133);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(257, 28);
            this.panel2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Max length";
            // 
            // largeFaceFilterMaxLength
            // 
            this.largeFaceFilterMaxLength.DecimalPlaces = 1;
            this.largeFaceFilterMaxLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.largeFaceFilterMaxLength.Location = new System.Drawing.Point(98, 3);
            this.largeFaceFilterMaxLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.largeFaceFilterMaxLength.Name = "largeFaceFilterMaxLength";
            this.largeFaceFilterMaxLength.Size = new System.Drawing.Size(156, 22);
            this.largeFaceFilterMaxLength.TabIndex = 0;
            this.largeFaceFilterMaxLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.largeFaceFilterMaxLength.ValueChanged += new System.EventHandler(this.largeFaceFilter_ParameterChanged);
            // 
            // largeFaceFilterRadioButton
            // 
            this.largeFaceFilterRadioButton.AutoSize = true;
            this.largeFaceFilterRadioButton.Location = new System.Drawing.Point(6, 106);
            this.largeFaceFilterRadioButton.Name = "largeFaceFilterRadioButton";
            this.largeFaceFilterRadioButton.Size = new System.Drawing.Size(128, 21);
            this.largeFaceFilterRadioButton.TabIndex = 3;
            this.largeFaceFilterRadioButton.TabStop = true;
            this.largeFaceFilterRadioButton.Text = "Large face filter";
            this.largeFaceFilterRadioButton.UseVisualStyleBackColor = true;
            this.largeFaceFilterRadioButton.CheckedChanged += new System.EventHandler(this.largeFaceFilter_ParameterChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.thinFaceFilterAngle);
            this.panel1.Location = new System.Drawing.Point(6, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 28);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Min angle";
            // 
            // thinFaceFilterAngle
            // 
            this.thinFaceFilterAngle.DecimalPlaces = 1;
            this.thinFaceFilterAngle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.thinFaceFilterAngle.Location = new System.Drawing.Point(98, 3);
            this.thinFaceFilterAngle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.thinFaceFilterAngle.Name = "thinFaceFilterAngle";
            this.thinFaceFilterAngle.Size = new System.Drawing.Size(156, 22);
            this.thinFaceFilterAngle.TabIndex = 0;
            this.thinFaceFilterAngle.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.thinFaceFilterAngle.ValueChanged += new System.EventHandler(this.thinFaceFilter_ParameterChanged);
            // 
            // thinFaceFilterRadioButton
            // 
            this.thinFaceFilterRadioButton.AutoSize = true;
            this.thinFaceFilterRadioButton.Location = new System.Drawing.Point(6, 48);
            this.thinFaceFilterRadioButton.Name = "thinFaceFilterRadioButton";
            this.thinFaceFilterRadioButton.Size = new System.Drawing.Size(126, 21);
            this.thinFaceFilterRadioButton.TabIndex = 1;
            this.thinFaceFilterRadioButton.Text = "Thin faces filter";
            this.thinFaceFilterRadioButton.UseVisualStyleBackColor = true;
            this.thinFaceFilterRadioButton.CheckedChanged += new System.EventHandler(this.thinFaceFilter_ParameterChanged);
            // 
            // noFaceFilterRadioButton
            // 
            this.noFaceFilterRadioButton.AutoSize = true;
            this.noFaceFilterRadioButton.Checked = true;
            this.noFaceFilterRadioButton.Location = new System.Drawing.Point(6, 21);
            this.noFaceFilterRadioButton.Name = "noFaceFilterRadioButton";
            this.noFaceFilterRadioButton.Size = new System.Drawing.Size(78, 21);
            this.noFaceFilterRadioButton.TabIndex = 0;
            this.noFaceFilterRadioButton.TabStop = true;
            this.noFaceFilterRadioButton.Text = "No filter";
            this.noFaceFilterRadioButton.UseVisualStyleBackColor = true;
            this.noFaceFilterRadioButton.CheckedChanged += new System.EventHandler(this.noFaceFilter_ParameterChanged);
            // 
            // meshRenderer1
            // 
            this.meshRenderer1.BackColor = System.Drawing.Color.Black;
            this.meshRenderer1.Location = new System.Drawing.Point(12, 12);
            this.meshRenderer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.meshRenderer1.Mesh = null;
            this.meshRenderer1.Name = "meshRenderer1";
            this.meshRenderer1.Scale = 1F;
            this.meshRenderer1.Size = new System.Drawing.Size(700, 700);
            this.meshRenderer1.TabIndex = 0;
            this.meshRenderer1.VSync = false;
            this.meshRenderer1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.meshRenderer1_MouseDown);
            this.meshRenderer1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.meshRenderer1_MouseMove);
            this.meshRenderer1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.meshRenderer1_MouseUp);
            // 
            // completeButton
            // 
            this.completeButton.Location = new System.Drawing.Point(719, 689);
            this.completeButton.Name = "completeButton";
            this.completeButton.Size = new System.Drawing.Size(269, 23);
            this.completeButton.TabIndex = 4;
            this.completeButton.Text = "Complete";
            this.completeButton.UseVisualStyleBackColor = true;
            this.completeButton.Click += new System.EventHandler(this.completeButton_Click);
            // 
            // PreviewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 724);
            this.Controls.Add(this.completeButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.meshRenderer1);
            this.Controls.Add(this.resetTransfromButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PreviewWindow";
            this.Text = "Preview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PreviewWindow_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.largeWidthFaceFilterMaxLength)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.largeFaceFilterMaxLength)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thinFaceFilterAngle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GraphicsUtils.MeshRenderer meshRenderer1;
        private System.Windows.Forms.Button resetTransfromButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown thinFaceFilterAngle;
        private System.Windows.Forms.RadioButton thinFaceFilterRadioButton;
        private System.Windows.Forms.RadioButton noFaceFilterRadioButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown largeFaceFilterMaxLength;
        private System.Windows.Forms.RadioButton largeFaceFilterRadioButton;
        private System.Windows.Forms.RadioButton largeWidthFaceFilterRadioButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown largeWidthFaceFilterMaxLength;
        private System.Windows.Forms.Button completeButton;
    }
}