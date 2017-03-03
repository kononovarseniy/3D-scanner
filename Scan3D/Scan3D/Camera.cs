using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
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

        public event EventHandler<NewFrameEventArgs> NewFrame;
        private void InvokeNewFrame(Bitmap frame)
        {
            var args = new NewFrameEventArgs(frame);
            NewFrame?.Invoke(this, args);
        }

        public Size FrameSize => videoSource.VideoCapabilities[0].FrameSize;

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
            InvokeNewFrame(e.Frame);
        }
    }
    //class VideoCaptureDevice1
    //{
    //    public delegate void FrameHandlerDelagate(Bitmap frame);

    //    public static List<VideoCaptureDeviceInfo> GetVideoCaptureDevices()
    //    {
    //        var videoDevices = new List<VideoCaptureDeviceInfo>();
    //        var collection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
    //        foreach (FilterInfo info in collection)
    //            videoDevices.Add(new VideoCaptureDeviceInfo(info.MonikerString, info.Name));
    //        return videoDevices;
    //    }

    //    private AForge.Video.DirectShow.VideoCaptureDevice videoSource;
    //    private HashSet<FrameContainer> startOnceFrameContainers = new HashSet<FrameContainer>();
    //    public event FrameHandlerDelagate NewFrame;

    //    public VideoCaptureDevice(VideoCaptureDeviceInfo deviceInfo)
    //    {
    //        videoSource = new AForge.Video.DirectShow.VideoCaptureDevice(deviceInfo.MonikerString);
    //        videoSource.NewFrame += videoSource_NewFrame;
    //        videoSource.Start();
    //        IsRunning = true;
    //    }

    //    public Size FrameSize => videoSource.VideoCapabilities[0].FrameSize;
    //    public bool IsRunning { get; private set; } = false;
    //    public void Stop()
    //    {
    //        if (IsRunning)
    //        {
    //            if (videoSource.IsRunning)
    //                videoSource.Stop();
    //            videoSource = null;
    //            IsRunning = false;
    //        }
    //    }

    //    private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
    //    {
    //        Bitmap frame = eventArgs.Frame;
    //        lock (startOnceFrameContainers)
    //        {
    //            foreach (var container in startOnceFrameContainers)
    //            {
    //                container.Frame = new Bitmap(frame);
    //            }
    //            startOnceFrameContainers.Clear();
    //        }
    //        // Обработчику события можно передать оригинал а не копию
    //        NewFrame(frame);
    //    }

    //    private class FrameContainer
    //    {
    //        public Bitmap Frame { get; set; } = null;
    //        public bool IsEmpty => Frame == null;
    //    }

    //    public async Task HandleFrameAsync(FrameHandlerDelagate handler)
    //    {
    //        var container = new FrameContainer();
    //        Action frameHandlerAction = () =>
    //        {
    //            while (container.IsEmpty) ;
    //            handler(container.Frame);
    //        };
    //        var frameHandlerTask = Task.Factory.StartNew(frameHandlerAction);

    //        lock (startOnceFrameContainers)
    //            startOnceFrameContainers.Add(container);

    //        await frameHandlerTask;
    //    }

    //    public Task<Bitmap> GetFrameAsync()
    //    {
    //        var container = new FrameContainer();

    //        lock (startOnceFrameContainers)
    //            startOnceFrameContainers.Add(container);

    //        return Task.Run(() =>
    //        {
    //            while (container.IsEmpty) ;
    //            return container.Frame;
    //        });
    //    }
    //}
}
