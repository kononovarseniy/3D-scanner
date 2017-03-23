using AForge.Video;
using AForge.Video.DirectShow;
using Ar.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scan3D
{
    public class VideoCaptureDeviceInfo
    {
        public string MonikerString { get; private set; }
        public string Name { get; private set; }
        public VideoCaptureDeviceInfo(string monikerString, string name)
        {
            MonikerString = monikerString;
            Name = name;
        }
    }
    public class NewFrameEventArgs : EventArgs
    {
        public Bitmap Frame { get; private set; }
        public NewFrameEventArgs(Bitmap frame)
        {
            Frame = frame;
        }
    }
    public class VideoCaptureDevice
    {
        private AForge.Video.DirectShow.VideoCaptureDevice videoSource;
        private bool isStarted;

        private Stopwatch frameStopwatch = new Stopwatch();
        private AverageQueue averageFramePeriod = new AverageQueue(5);

        public event EventHandler<NewFrameEventArgs> NewFrame;
        private void InvokeNewFrame(Bitmap frame)
        {
            var args = new NewFrameEventArgs(frame);
            NewFrame?.Invoke(this, args);
        }

        public Size FrameSize => videoSource.VideoCapabilities[0].FrameSize;
        public double FramePeriod
        {
            get
            {
                lock (averageFramePeriod)
                    return averageFramePeriod.Value;
            }
        }

        public static List<VideoCaptureDeviceInfo> GetDevices()
        {
            var videoDevices = new List<VideoCaptureDeviceInfo>();
            var collection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo info in collection)
                videoDevices.Add(new VideoCaptureDeviceInfo(info.MonikerString, info.Name));
            return videoDevices;
        }

        public VideoCaptureDevice(VideoCaptureDeviceInfo deviceInfo)
        {
            videoSource = new AForge.Video.DirectShow.VideoCaptureDevice(deviceInfo.MonikerString);
            videoSource.NewFrame += videoSource_NewFrame;
            videoSource.Start();
            isStarted = true;
        }

        public void Stop()
        {
            if (isStarted)
            {
                if (videoSource.IsRunning)
                    videoSource.Stop();
                videoSource = null;
                isStarted = false;
            }
        }

        private void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs e)
        {
            double ellapsed = frameStopwatch.ElapsedMilliseconds;
            if (frameStopwatch.IsRunning)
            {
                lock(averageFramePeriod)
                    averageFramePeriod.Enqueue(ellapsed);
            }
            frameStopwatch.Restart();
            InvokeNewFrame(e.Frame);
        }
    }
}
