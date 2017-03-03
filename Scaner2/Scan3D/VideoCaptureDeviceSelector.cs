using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scan3D
{
    public partial class VideoCaptureDeviceSelector : UserControl
    {
        public event EventHandler<VideoDeviceSelectedEventArgs> DeviceSelected;
        private void InvokeDeviceSelected(VideoCaptureDeviceInfo device)
        {
            var args = new VideoDeviceSelectedEventArgs(device);
            DeviceSelected?.Invoke(this, args);
        }

        public VideoCaptureDeviceSelector()
        {
            InitializeComponent();
        }
        
        public void Update()
        {
            var devices = VideoCaptureDevice.GetDevices().ToArray();
            deviceSelectComboBox.Items.Clear();
            deviceSelectComboBox.Items.AddRange(devices);
            bool selected = devices.Length > 0;
            deviceSelectComboBox.SelectedIndex = selected ? 0 : -1;
            selectButton.Enabled = selected;
        }

        public void Select()
        {
            var device = (VideoCaptureDeviceInfo)deviceSelectComboBox.SelectedItem;
            InvokeDeviceSelected(device);
        }

        private void VideoCaptureDeviceSelector_Load(object sender, EventArgs e)
        {
            Update();
        }

        private void deviceSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectButton.Enabled = deviceSelectComboBox.SelectedIndex > -1;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            Select();
        }
    }

    public class VideoDeviceSelectedEventArgs : EventArgs
    {
        public VideoCaptureDeviceInfo Device { get; private set; }

        public VideoDeviceSelectedEventArgs(VideoCaptureDeviceInfo device)
        {
            Device = device;
        }
    }
}
