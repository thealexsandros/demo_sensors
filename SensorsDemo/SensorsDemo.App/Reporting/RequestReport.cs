using System;
using System.Diagnostics;
using System.Threading;

using NLog;

namespace AlexSandros.SensorsDemo.App.Reporting
{
    class RequestReport
    {
        private int _processedRequests;

        private int _successfulRequests;

        private Stopwatch _stopwatch = null;

        static private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Start()
        {
            _stopwatch = Stopwatch.StartNew();
            _processedRequests = 0;
        }

        public void IncrementProcessed(bool isSuccessful = true)
        {
            Interlocked.Increment(ref _processedRequests);
            if (isSuccessful)
            {
                Interlocked.Increment(ref _successfulRequests);
            }
        }

        public void StopAndLog(TimeSpan maxStepLength)
        {
            int processedRequests = 0;
            int successfulRequests = 0;
            long elapsedMilliseconds = 0;
            if (_stopwatch != null)
            {
                processedRequests = Interlocked.Exchange(ref _processedRequests, 0);
                successfulRequests = Interlocked.Exchange(ref _successfulRequests, 0);
                _stopwatch.Stop();
                elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            }

            // Info использовано для простоты - чтобы не конфигурировать цвета в NLog.config.
            bool timedOut = elapsedMilliseconds > maxStepLength.TotalMilliseconds;
            if (timedOut)
            {
                _logger.Error($"Processed {processedRequests} requests (successful: {successfulRequests}) in {elapsedMilliseconds} ms. TIMED OUT!");

            }
            else
            {
                _logger.Info($"Processed {processedRequests} requests (successful: {successfulRequests}) in {elapsedMilliseconds} ms.");
            }
        }
    }
}
