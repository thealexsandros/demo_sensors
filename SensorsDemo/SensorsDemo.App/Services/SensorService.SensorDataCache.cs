using System;
using System.Collections.Generic;
using System.Linq;

using AlexSandros.SensorsDemo.App.Contracts.Internal;
using AlexSandros.SensorsDemo.App.Contracts.Public;

namespace AlexSandros.SensorsDemo.App.Services
{
    partial class SensorService
    {
        private class SensorDataCache : ISensorDataReadOnlyCache
        {
            private IReadOnlyDictionary<int, SensorDataCacheItem> _sensorIdToDataCacheItemIndex;

            public IReadOnlyCollection<int> SensorIds { get; }

            public SensorDataCache(IReadOnlyCollection<int> sensorIds)
            {
                this.SensorIds = sensorIds;

                _sensorIdToDataCacheItemIndex = sensorIds.ToDictionary(x => x, _ => new SensorDataCacheItem());
            }

            public bool TryGetSensorData(int sensorId, out SensorData sensorData, out DateTime? sensorDataLastUpdateDateTimeUtc)
            {
                if (_sensorIdToDataCacheItemIndex.TryGetValue(sensorId, out SensorDataCacheItem sensorDataCacheItem))
                {
                    sensorDataCacheItem.GetData(out sensorData, out sensorDataLastUpdateDateTimeUtc);
                    return true;
                }

                sensorData = null;
                sensorDataLastUpdateDateTimeUtc = null;
                return false;
            }

            public void UpdateSensorData(int sensorId, SensorData sensorData)
            {
                if (_sensorIdToDataCacheItemIndex.TryGetValue(sensorId, out SensorDataCacheItem sensorDataCacheItem))
                {
                    sensorDataCacheItem.UpdateData(sensorData);
                    return;
                }

                throw new ArgumentOutOfRangeException(nameof(sensorId));
            }
        }
    }
}
