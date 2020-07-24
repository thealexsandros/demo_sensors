using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NLog;

using AlexSandros.SensorsDemo.App.Contracts.Public;
using AlexSandros.SensorsDemo.App.Reporting;

namespace AlexSandros.SensorsDemo.App.Demo
{
    class DemoUtils
    {
        static private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        static public void SendTestRequests(
            IReadOnlyList<int> sensorIds,
            ISensorService sensorService,
            int requestCount,
            int repeatCount,
            TimeSpan maxStepLength,
            RequestReport report)
        {
            var random = new Random();
            for (int i = 0; i < repeatCount; i++)
            {
                List<GetSensorDataParameters> parameters = Enumerable
                    .Repeat(0, requestCount)
                    .Select(_ => new GetSensorDataParameters { SensorId = sensorIds[random.Next(sensorIds.Count)] })
                    .ToList();

                // Debug использован для простоты - чтобы не конфигурировать цвета в NLog.config.
                _logger.Debug($"Trying to send {requestCount} requests simultaneously (attempt #{i + 1} of {repeatCount}).");

                report.Start();

                var tasks = parameters
                    .Select(x => Task.Run(() => sensorService.GetSensorData(x)))
                    .ToArray();

                Task.WaitAll(tasks);
                report.StopAndLog(maxStepLength);
            }
        }
    }
}
