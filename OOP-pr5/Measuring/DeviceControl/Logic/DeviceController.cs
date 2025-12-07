namespace DeviceControl
{
    public class DeviceController : IDeviceController
    {
        private bool isActive = false;
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
            if (isActive) return (int)(generator!.NextDouble()*100);
            else return -1;
        }
    }
}