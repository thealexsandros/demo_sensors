using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using AlexSandros.SensorsDemo.App.Contracts.Internal;
using AlexSandros.SensorsDemo.App.Contracts.Public;

namespace AlexSandros.SensorsDemo.App.DataProviders
{
    class RandomSensorDataProvider : ISensorDataProvider
    {
        private ImmutableHashSet<int> SensorIds { get; }

        private TimeSpan MaxExpectedSensorCallbackInterval { get; }

        public RandomSensorDataProvider(
            IReadOnlyCollection<int> sensorIds,
            TimeSpan maxExpectedSensorCallbackInterval)
        {
            this.SensorIds = new HashSet<int>(sensorIds).ToImmutableHashSet();
            this.MaxExpectedSensorCallbackInterval = maxExpectedSensorCallbackInterval;
        }

        /// <inheritdoc />
        public async Task<SensorData> GetSensorDataAsync(int sensorId)
        {
            var random = new Random();
            int maxMilliseconds = (int)this.MaxExpectedSensorCallbackInterval.TotalMilliseconds;
            int delayMilliseconds = random.Next(maxMilliseconds + 1);
            await Task.Delay(delayMilliseconds);

            if (this.SensorIds.Contains(sensorId))
            {
                return new SensorData(
                    temperature: random.Next(Consts.MaxSensorValue + 1),
                    humidity: random.Next(Consts.MaxSensorValue + 1),
                    gasPollution: random.Next(Consts.MaxSensorValue + 1)
                );
            }

            return null;
        }
    }
}
