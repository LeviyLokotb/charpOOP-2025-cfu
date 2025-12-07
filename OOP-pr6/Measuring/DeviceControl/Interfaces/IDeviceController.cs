namespace DeviceControl
{
    public interface IDeviceController
    {
        /// <summary>
        /// Эмулирует измерение прибором
        /// </summary>
        /// <returns>Измеренное значение</returns>
        int TakeMeasurement();
        /// <summary>
        /// Создаёт объект эмулятора прибора и запускает его
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        static abstract DeviceController StartDevice(DeviceType deviceType);
        /// <summary>
        /// Останавливает прибор
        /// </summary>
        void StopDevice();
        public bool IsActive { get; }
    }
}