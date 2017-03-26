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
            this.completeButton = new System.Windows.Forms.Button();
            this.meshRenderer1 = new Scan3D.GraphicsUtils.MeshRenderer();
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
            // completeButton
            // 
            this.completeButton.Location = new System.Drawing.Point(719, 689);
            this.completeButton.Name = "completeButton";
            this.completeButton.Size = new System.Drawing.Size(269, 23);
            this.completeButton.TabIndex = 2;
            this.completeButton.Text = "OK";
            this.completeButton.UseVisualStyleBackColor = true;
            this.completeButton.Click += new System.EventHandler(this.completeButton_Click);
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
            // PreviewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 724);
            this.Controls.Add(this.completeButton);
            this.Controls.Add(this.meshRenderer1);
            this.Controls.Add(this.resetTransfromButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PreviewWindow";
            this.Text = "Preview";
            this.ResumeLayout(false);

        }

        #endregion

        private GraphicsUtils.MeshRenderer meshRenderer1;
        private System.Windows.Forms.Button resetTransfromButton;
        private System.Windows.Forms.Button completeButton;
    }
}