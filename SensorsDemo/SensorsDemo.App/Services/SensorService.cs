using System;
using System.Collections.Generic;
using System.Threading;
using AlexSandros.SensorsDemo.App.Contracts.Internal;
using AlexSandros.SensorsDemo.App.Contracts.Public;
using AlexSandros.SensorsDemo.App.Reporting;

namespace AlexSandros.SensorsDemo.App.Services
{
    partial class SensorService : ISensorService
    {
        #pragma warning disable IDE0052 // Поле необходимо для сохранения ссылки на модуль на время работы сервиса.

        private readonly RefreshDataModule _refreshDataModule;

        #pragma warning restore IDE0052

        private ISensorDataReadOnlyCache Cache { get; }

        private RequestReport Report { get; }

        public SensorService(
            TimeSpan refreshInterval,
            IReadOnlyCollection<int> sensorIds,
            ISensorDataProvider sensorDataProvider,
            RequestReport requestReport,
            CancellationToken cancelRefreshSensorsToken)
        {
            var cache = new SensorDataCache(sensorIds);
            _refreshDataModule = new RefreshDataModule(
                refreshInterval,
                sensorDataProvider,
                cache,
                cancelRefreshSensorsToken
            );

            this.Cache = cache;
            this.Report = requestReport;
        }

        /// <inheritdoc />
        public GetSensorDataResult GetSensorData(GetSensorDataParameters parameters)
        {
            bool gotSensorData = this.Cache.TryGetSensorData(
                parameters.SensorId,
                out SensorData sensorData,
                out DateTime? sensorDataLastUpdateDateTimeUtc
            );

            if (gotSensorData && sensorData != null)
            {
                this.Report?.IncrementProcessed();
                return new GetSensorDataResult
                {
                    Parameters = parameters,
                    SensorData = sensorData,
                    SensorDataLastUpdateDateTimeUtc = sensorDataLastUpdateDateTimeUtc
                };
            }

            if (gotSensorData && sensorData == null)
            {
                this.Report?.IncrementProcessed(isSuccessful: false);
                return new GetSensorDataResult
                {
                    Parameters = parameters,
                    NoDataFromSensor = true
                };
            }

            this.Report?.IncrementProcessed(isSuccessful: false);
            return new GetSensorDataResult
            {
                Parameters = parameters,
                InvalidSensorId = true
            };
        }
    }
}
