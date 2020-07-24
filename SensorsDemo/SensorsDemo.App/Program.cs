using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlexSandros.SensorsDemo.App.DataProviders;
using AlexSandros.SensorsDemo.App.Demo;
using AlexSandros.SensorsDemo.App.Reporting;
using AlexSandros.SensorsDemo.App.Services;

namespace AlexSandros.SensorsDemo.App
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> sensorIds = Enumerable
                .Range(0, Configuration.SensorCount)
                .ToList();

            var sensorDataProvider = new RandomSensorDataProvider(
                sensorIds: sensorIds,
                maxExpectedSensorCallbackInterval: Configuration.MaxExpectedSensorCallbackInterval
            );

            var report = new RequestReport();

            using var cancelRefreshSensorsTokenSource = new CancellationTokenSource();
            var sensorService = new SensorService(
                refreshInterval: Configuration.RefreshInterval,
                sensorIds: sensorIds,
                sensorDataProvider: sensorDataProvider,
                requestReport: report,
                cancelRefreshSensorsToken: cancelRefreshSensorsTokenSource.Token
            );

            Task.Delay(Configuration.RefreshInterval).Wait();
            Console.WriteLine("Initialized.");

            for (int requestCount = Configuration.MinRequests; requestCount <= Configuration.MaxRequests; requestCount = Configuration.GetNextStepRequests(requestCount))
            {
                DemoUtils.SendTestRequests(
                    sensorIds: sensorIds,
                    sensorService: sensorService,
                    requestCount: requestCount,
                    repeatCount: Configuration.StepRepeatCount,
                    maxStepLength: Configuration.StepLength,
                    report: report
                );
            }

            cancelRefreshSensorsTokenSource.Cancel();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
