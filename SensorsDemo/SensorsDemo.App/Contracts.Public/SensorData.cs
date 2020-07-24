using System;

namespace AlexSandros.SensorsDemo.App.Contracts.Public
{
    public class SensorData
    {
        /// <summary>
        /// Возвращает значение температуры.
        /// </summary>
        /// <remarks>
        /// Значение находится в диапазоне 0 - <see cref="Consts.MaxSensorValue"/>.
        /// </remarks>
        public int Temperature { get; }

        /// <summary>
        /// Возвращает значение влажности.
        /// </summary>
        /// <remarks>
        /// Значение находится в диапазоне 0 - <see cref="Consts.MaxSensorValue"/>.
        /// </remarks>
        public int Humidity { get; }

        /// <summary>
        /// Возвращает значение загазованности.
        /// </summary>
        /// <remarks>
        /// Значение находится в диапазоне 0 - <see cref="Consts.MaxSensorValue"/>.
        /// </remarks>
        public int GasPollution { get; }

        public SensorData(int temperature, int humidity, int gasPollution)
        {
            Validate(
                temperature: temperature,
                humidity: humidity,
                gasPollution: gasPollution
            );

            this.Temperature = temperature;
            this.Humidity = humidity;
            this.GasPollution = gasPollution;
        }

        static private void Validate(int temperature, int humidity, int gasPollution)
        {
            if (temperature > Consts.MaxSensorValue)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(temperature));
            }

            if (humidity > Consts.MaxSensorValue)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(humidity));
            }

            if (gasPollution > Consts.MaxSensorValue)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(gasPollution));
            }
        }
    }
}
