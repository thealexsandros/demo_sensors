using System;
using System.Threading.Tasks;
using System.Timers;

using AlexSandros.SensorsDemo.App.Contracts.Internal;
using AlexSandros.SensorsDemo.App.Contracts.Public;
using AlexSandros.SensorsDemo.App.Reporting;

using CancellationToken = System.Threading.CancellationToken;

namespace AlexSandros.SensorsDemo.App.Services
{
    partial class SensorService
    {
        private class RefreshDataModule
        {
            private TimeSpan RefreshInterval { get; }

            private ISensorDataProvider SensorDataProvider { get; }

            private Timer RefreshTimer { get; }

            private SensorDataCache Cache { get; }

            private UpdateSensorDataReport Report { get; }

            public RefreshDataModule(
                TimeSpan refreshInterval,
                ISensorDataProvider sensorDataProvider,
                SensorDataCache cache,
                CancellationToken cancelRefreshSensorsToken)
            {
                this.RefreshInterval = refreshInterval;
                this.Cache = cache;
                this.SensorDataProvider = sensorDataProvider;
                this.Report = new UpdateSensorDataReport();

                RefreshSensorsData();

                this.RefreshTimer = new Timer
                {
                    AutoReset = true,
                    Interval = this.RefreshInterval.TotalMilliseconds
                };

                this.RefreshTimer.Elapsed += RefreshTimer_Elapsed;
                this.RefreshTimer.Start();

                cancelRefreshSensorsToken.Register(
                    () =>
                    {
                        this.RefreshTimer.Stop();
                        this.RefreshTimer.Elapsed -= RefreshTimer_Elapsed;
                        this.Report.LogState(updateStopped: true);
                    }
                );

            }

            private void RefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
            {
                RefreshSensorsData();
            }

            private void RefreshSensorsData()
            {
                this.Report.LogState();

                foreach (int sensorId in this.Cache.SensorIds)
                {
#pragma warning disable CS4014 // Необходимо просто запустить задачу. Не требуется дожидаться её завершения.

                    RefreshSensorDataAsync(sensorId);

#pragma warning restore CS4014
                }
            }

            private async Task RefreshSensorDataAsync(int sensorId)
            {
                Task<SensorData> getSensorData = this.SensorDataProvider.GetSensorDataAsync(sensorId);
                bool getSensorDataCompleted = await Task.WhenAny(getSensorData, Task.Delay(this.RefreshInterval)) == getSensorData;
                if (getSensorDataCompleted)
                {
                    this.Cache.UpdateSensorData(sensorId, await getSensorData);
                    this.Report.IncrementUpdated();
                }
                else
                {
                    this.Report.IncrementTimedOut();
                }
            }
        }
    }
}
