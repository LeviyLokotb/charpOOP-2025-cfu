namespace DeviceControl
{
    public class DeviceController : IDeviceController
    {
        private bool isActive = false;
        public bool IsActive => isActive;
        private DeviceType deviceType;
        private Random? generator;
        public DeviceController(DeviceType deviceType, bool isActive = false)
        {
            this.isActive = isActive;
            this.generator = new Random();
            this.deviceType = deviceType;
        }
        public static DeviceController StartDevice(DeviceType deviceType)
        {
            return new DeviceController(deviceType, true);
        }

        public void StopDevice()
        {
            this.isActive = false;
        }
        public int TakeMeasurement()
        {
            if (generator != null && isActive)
            {
                Thread.Sleep(generator.Next(500, 1000));
                return (int)(generator.NextDouble()*100);
            }
            return -1;
        }
    }
}