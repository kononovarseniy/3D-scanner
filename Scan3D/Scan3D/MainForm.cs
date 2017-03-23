using AForge.Imaging;
using AForge.Imaging.Filters;
using Ar.Utils;
using Ar.Serial.Gui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scan3D
{
    public partial class MainForm : Form
    {
        private static class Properties
        {
            public const int PlatformY = 46;
            public const int CamZ = -410;
            public const int CamY = 210 - PlatformY;
            public const int LaserAngle = 45;
            public const int DefaultCameraYaw = 0;
            public const int DefaultCameraPitch = -10;
            // Camera
            public const int FrameWidth = 640;
            public const int FrameHeight = 480;
            public const int HorizontalFov = 30; // ?
            public const double Ratio = (double)FrameWidth / FrameHeight;
            // Settings
            public const int PlatformStep = 1;
            public const float CylinderBottom = 0;
            public const float CylinderTop = 200;
            public const float CylinderRadius = 100;
            public static readonly Cylinder Cylinder = new Cylinder(CylinderBottom, CylinderTop, CylinderRadius);
        }

        private DeviceController Device;
        private VideoCaptureDevice VideoDevice = null;

        private PointScanner PointScanner = new PointScanner()
        {
            FovX = Properties.HorizontalFov * Math.PI / 180,
            Ratio = Properties.Ratio,
            CameraPosition = new Vector3(0, Properties.CamY, Properties.CamZ),
            CameraPitch = Properties.DefaultCameraPitch * Math.PI / 180,
            CameraYaw = Properties.DefaultCameraYaw * Math.PI / 180,
            LaserAngle = Properties.LaserAngle * Math.PI / 180
        };
        private HighlitedPointDetector PointDetector = new HighlitedPointDetector()
        {
            //AverageItems = 5,
            //MaxSpace = 3,
            //Threshold = 120
        };

        public MainForm()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            var devices = VideoCaptureDevice.GetDevices();

        }

        private void videoCaptureDeviceSelector1_DeviceSelected(object sender, VideoDeviceSelectedEventArgs e)
        {
            if (VideoDevice != null)
            {
                VideoDevice.Stop();
                VideoDevice.NewFrame -= VideoDevice_NewFrame;
            }
            VideoDevice = new VideoCaptureDevice(e.Device);
            VideoDevice.NewFrame += VideoDevice_NewFrame;
        }
        
        private void VideoDevice_NewFrame(object sender, NewFrameEventArgs e)
        {
            var source = new Bitmap(e.Frame);
            var unmanaged = UnmanagedImage.FromManagedImage(source);

            Grayscale grayscaleFilter = new Grayscale(1, 0, 0);
            var grayscaleUnm = grayscaleFilter.Apply(unmanaged);
            var grayscale = grayscaleUnm.ToManagedImage();

            var points = PointDetector.FindHighlitedPoints(grayscaleUnm.ImageData, e.Frame.Width, e.Frame.Height);
            var points3Enumerable = from point in points
                                    select PointScanner.ConvertTo3D(point, e.Frame.Width, e.Frame.Height) into point3
                                    where Properties.Cylinder.Contains(point3)
                                    select point3;
            Vector3[] vertices = points3Enumerable.ToArray();
            using (var gr = Graphics.FromImage(source))
            {
                foreach (var p in points)
                {
                    gr.FillEllipse(Brushes.Green, p.X - 4, p.Y - 4, 8, 8);
                }
                foreach (var v in vertices)
                {
                    var p2 = PointScanner.ConvertTo2D(v, e.Frame.Width, e.Frame.Height);
                    Brush color = v.Z > 0 ? Brushes.Blue : Brushes.Red;
                    gr.FillEllipse(color, p2.X - 2, p2.Y - 2, 4, 4);
                    /*var point = Vector3.Transform(v, rotation);
                    float px = (point.X / 200 + 1) / 2;
                    float py = (point.Y / 200 + 1) / 2;
                    float spx = px * e.Frame.Width;
                    float spy = (1 - py) * e.Frame.Height;
                    gr.FillEllipse(point.Z > 0 ? Brushes.Red : Brushes.Blue, spx - 4, spy - 4, 8, 8);*/
                }
            }

            using (var graphics = sourceImage.CreateGraphics())
                graphics.DrawImage(source, 0, 0, sourceImage.Width, sourceImage.Height);
            using (var graphics = grayscaleImage.CreateGraphics())
                graphics.DrawImage(grayscale, 0, 0, sourceImage.Width, sourceImage.Height);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            VideoDevice?.Stop();
        }

        private void sourceImage_MouseDown(object sender, MouseEventArgs e)
        {
            float x = e.X;
            float y = e.Y;
            int w = sourceImage.Width;
            int h = sourceImage.Height;
            
            if (e.Button == MouseButtons.Right)
            {
                PointScanner.Calibrate(x, y, w, h);
                int pitch = (int)(PointScanner.CameraPitch / Math.PI * 180);
                int yaw = (int)(PointScanner.CameraYaw / Math.PI * 180);
                calibrationStateLabel.Text = $"Calibrated! Pitch: {pitch}; Yaw: {yaw}";
            }
            else
            {
                Vector3 p = PointScanner.ConvertTo3D(x, y, w, h);
                calibrationStateLabel.Text = $"X: {(int)p.X}mm; Y: {(int)p.Y}mm; Z: {(int)p.Z}mm;";
            }
        }
        
        private async void selectSerialButton_Click(object sender, EventArgs e)
        {
            var dlg = new OpenSerialPortDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (Device != null)
                {
                    await Device.Stop();
                    Device.Dispose();
                    Device = null;
                }
                Device = new DeviceController(dlg.Result);
                int version = await Device.GetVersionAsync();
                await Device.SetStepDelay1Async(2000);
                await Device.SetStepDelay2Async(2000);
                await Device.Setup();
                directControlBox.Enabled = true;
            }
        }
                
        private async void scanButton_Click(object sender, EventArgs e)
        {
            Scanner scanner = new Scanner(
                Device,
                Properties.PlatformStep * Math.PI / 180,
                VideoDevice,
                PointDetector,
                PointScanner,
                Properties.Cylinder);
            DateTime startTime = DateTime.Now;
            Mesh mesh = await scanner.Scan();
            DateTime stopTime = DateTime.Now;
            MessageBox.Show($"Complete!!!\r\nStart: {startTime}\r\nStop: {stopTime}\r\nTotal: {stopTime - startTime}", "Success");

            SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                mesh.WriteToFile(
                    Path.GetDirectoryName(dlg.FileName),
                    Path.GetFileName(dlg.FileName));
            }
        }
        


        private async Task ExecuteRobotAction(Func<Task> action)
        {
            if (Device == null || Device.IsBusy) return;
            directControlBox.Enabled = false;
            statusLabel.Text = "Working...";
            await action();
            directControlBox.Enabled = true;
            statusLabel.Text = "Done!!!";
        }

        private async void rotateButton_Click(object sender, EventArgs e)
        {
            await ExecuteRobotAction(async () =>
            {
                await Device.Start();
                await Device.Rotate((double)numericUpDown1.Value * Math.PI / 180);
                await Device.Stop();
            });
        }

        private async void lightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            await ExecuteRobotAction(async () =>
            {
                await Device.SetLedState(lightCheckBox.Checked);
            });
        }

        private async void laserCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            await ExecuteRobotAction(async () =>
            {
                await Device.SetLaserState(laserCheckBox.Checked);
            });
        }
    }
}
