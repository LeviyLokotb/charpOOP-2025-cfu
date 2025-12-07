namespace MeasuringDevice
{
    using System;
    public class HeartBeatEventArgs : EventArgs
    {
        public DateTime TimeStamp { get; }
        public HeartBeatEventArgs() : base()
        {
            TimeStamp = DateTime.Now;
        }
    }

    public delegate void HeartBeatEventHandler(object sender, HeartBeatEventArgs args);
}