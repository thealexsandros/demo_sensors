using System;
using System.Collections.Generic;

using AlexSandros.SensorsDemo.App.Contracts.Public;

namespace AlexSandros.SensorsDemo.App.Services
{
    partial class SensorService
    {
        interface ISensorDataReadOnlyCache
        {
            IReadOnlyCollection<int> SensorIds { get; }

            bool TryGetSensorData(int sensorId, out SensorData sensorData, out DateTime? sensorDataLastUpdateDateTimeUtc);
        }
    }
}
