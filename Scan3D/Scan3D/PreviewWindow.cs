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
using System.IO;

namespace Scan3D
{
    public partial class PreviewWindow : Form
    {
        private float yaw = 0;
        private float pitch = 0;
        private bool saved = false;

        private MouseButtons pressedMouseButtons = MouseButtons.None;
        private Point previousLocation = Point.Empty;

        private Mesh _mesh;
        public Mesh Mesh
        {
            get
            {
                return _mesh;
            }
            set
            {
                _mesh = value;
                ResultMesh = value?.Clone();
            }
        }
        private Mesh _resultMesh;
        public Mesh ResultMesh
        {
            get
            {
                return _resultMesh;
            }
            set
            {
                _resultMesh = value;
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


        private bool Save()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ResultMesh.WriteToFile(
                    Path.GetDirectoryName(dlg.FileName),
                    Path.GetFileName(dlg.FileName));
                saved = true;
                return true;
            }
            return false;
        }

        private bool ConfirmExit()
        {
            if (saved) return true;
            var answer = MessageBox.Show(
                    "Unsaved changes. Would you like to save?",
                    "Warning!",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button3);
            switch (answer)
            {
                case DialogResult.Cancel:
                    return false;
                case DialogResult.Yes:
                    return Save();
                case DialogResult.No:
                    return true;
                default:
                    return false;
            }
        }
        
        private void ApplyFilter(IFaceFilter filter)
        {
            if (Mesh == null) return;
            ResultMesh = Mesh.Clone();
            if (filter != null)
                ResultMesh.FilterFaces(filter);
            saved = false;
            meshRenderer1.Invalidate();
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            Save();
        }
        
        private void completeButton_Click(object sender, EventArgs e)
        {
            if (ConfirmExit())
            {
                Close();
            }
        }
        

        private void noFaceFilter_ParameterChanged(object sender, EventArgs e)
        {
            if (noFaceFilterRadioButton.Checked)
            {
                ApplyFilter(null);
            }
        }

        private void thinFaceFilter_ParameterChanged(object sender, EventArgs e)
        {
            if (thinFaceFilterRadioButton.Checked)
            {
                double minAngle = (double)thinFaceFilterAngle.Value * Math.PI / 180;
                ApplyFilter(new ThinFaceFilter(minAngle));
            }
        }

        private void largeFaceFilter_ParameterChanged(object sender, EventArgs e)
        {
            if (largeFaceFilterRadioButton.Checked)
            {
                double maxLength = (double)largeFaceFilterMaxLength.Value;
                ApplyFilter(new LargeFaceFilter(maxLength));
            }
        }

        private void largeWidthFaceFilter_ParameterChandeg(object sender, EventArgs e)
        {
            if (largeWidthFaceFilterRadioButton.Checked)
            {
                double maxLength = (double)largeWidthFaceFilterMaxLength.Value;
                ApplyFilter(new LargeWidthFaceFilter(maxLength));
            }
        }

        private void PreviewWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
            e.Cancel = !ConfirmExit();
        }
    }
}