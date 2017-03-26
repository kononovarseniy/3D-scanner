using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using Scan3D.GraphicsUtils;

namespace Scan3D
{
    public partial class PreviewWindow : Form
    {
        private float yaw = 0;
        private float pitch = 0;

        private MouseButtons pressedMouseButtons = MouseButtons.None;
        private Point previousLocation = Point.Empty;

        public Mesh Mesh
        {
            get
            {
                return meshRenderer1.Mesh;
            }
            set
            {
                meshRenderer1.Mesh = value;
            }
        }

        public PreviewWindow() : this(null) { }

        public PreviewWindow(Mesh mesh)
        {
            InitializeComponent();
            Mesh = mesh;
            meshRenderer1.MouseWheel += meshRenderer1_MouseWheel;
        }

        public void InvalidateRenderWindow()
        {
            meshRenderer1.Invalidate();
        }

        private void meshRenderer1_MouseDown(object sender, MouseEventArgs e)
        {
            pressedMouseButtons |= e.Button;
        }

        private void meshRenderer1_MouseUp(object sender, MouseEventArgs e)
        {
            pressedMouseButtons &= ~e.Button;
        }
        
        private void meshRenderer1_MouseWheel(object sender, MouseEventArgs e)
        {
            meshRenderer1.Scale += e.Delta / 120 * 0.1f;
            meshRenderer1.Invalidate();
        }

        private void meshRenderer1_MouseMove(object sender, MouseEventArgs e)
        {
            float dx = e.Location.X - previousLocation.X;
            float dy = e.Location.Y - previousLocation.Y;
            bool invalidate = false;
            if (pressedMouseButtons == MouseButtons.Left)
            {
                yaw += dx / 100;
                pitch += dy / 100;
                meshRenderer1.Rotation =
                    Quaternion.CreateFromYawPitchRoll(0, pitch, 0) *
                    Quaternion.CreateFromYawPitchRoll(yaw, 0, 0);
                invalidate = true;
            }
            else if (pressedMouseButtons == MouseButtons.Right)
            {
                var relativeTranslation = new Vector3(dx / 3, -dy / 3, 0);
                var scale = meshRenderer1.Scale;
                var rotation = meshRenderer1.Rotation;
                var scaled = Vector3.Multiply(relativeTranslation, 1 / scale);
                var rotated = Vector3.Transform(scaled, Quaternion.Inverse(rotation));
                meshRenderer1.Translation += rotated;
                invalidate = true;
            }
            previousLocation = e.Location;
            if (invalidate)
                meshRenderer1.Invalidate();
        }

        private void resetTransfromButton_Click(object sender, EventArgs e)
        {
            meshRenderer1.Scale = 1;
            meshRenderer1.Rotation = Quaternion.Identity;
            meshRenderer1.Translation = Vector3.Zero;
            meshRenderer1.Invalidate();
        }

        private void completeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
;