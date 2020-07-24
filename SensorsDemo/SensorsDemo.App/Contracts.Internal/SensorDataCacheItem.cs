using System;

using AlexSandros.SensorsDemo.App.Contracts.Public;

namespace AlexSandros.SensorsDemo.App.Contracts.Internal
{
    class SensorDataCacheItem
    {
        private class Data
        {
            public SensorData SensorData { get; }

            public DateTime SensorDataLastUpdateDateTimeUtc { get; }

            public Data(SensorData sensorData)
            {
                this.SensorData = sensorData ?? throw new ArgumentNullException(nameof(sensorData));
                this.SensorDataLastUpdateDateTimeUtc = DateTime.UtcNow;
            }
        }

        private volatile Data _data;

        public void UpdateData(SensorData sensorData)
        {
            _data = new Data(sensorData);
        }

        public void GetData(out SensorData sensorData, out DateTime? sensorDataLastUpdateDateTimeUtc)
        {
            Data data = _data;
            sensorData = data?.SensorData;
            sensorDataLastUpdateDateTimeUtc = data?.SensorDataLastUpdateDateTimeUtc;
        }
    }
}
