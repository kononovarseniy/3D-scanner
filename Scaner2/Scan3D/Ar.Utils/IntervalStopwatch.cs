using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ar.Utils
{
    public class IntervalStopwatch
    {
        public struct Record
        {
            public string Comment { get; private set; }
            public long Ticks { get; private set; }
            public double Nanoseconds
            {
                get
                {
                    return Ticks * 1e6 / Stopwatch.Frequency;
                }
            }
            public double Milliseconds
            {
                get
                {
                    return Ticks * 1e3 / Stopwatch.Frequency;
                }
            }
            public double Seconds
            {
                get
                {
                    return Ticks * 1e0 / Stopwatch.Frequency;
                }
            }

            public Record(long ticks, string comment)
            {
                Comment = comment;
                Ticks = ticks;
            }
        }

        private Stopwatch sw = new Stopwatch();

        public List<Record> Records = new List<IntervalStopwatch.Record>();
        public long Milliseconds { get { return sw.ElapsedMilliseconds; } }
        private long prevTicks = 0;

        public IntervalStopwatch(bool startImmediately = false)
        {
            if (startImmediately)
            {
                Start();
            }
        }

        public void Start() { sw.Start(); }
        public void Stop() { sw.Stop(); }
        public void MakeRecord(string comment = "", bool stop = false)
        {
            sw.Stop();
            Records.Add(
                new Record(sw.ElapsedTicks - prevTicks, comment));
            prevTicks = sw.ElapsedTicks;
            if (!stop) sw.Start();
        }

        public override string ToString()
        {
            return string.Format(
                "Total: {0}\r\n{1}",
                Milliseconds,
                string.Join(
                    "\r\n",
                    Records.Select(
                        (r, i) => string.Format("[{0}] {1} {2}", i, (int)r.Milliseconds, r.Comment))
                        .ToArray()));
        }
    }
}
