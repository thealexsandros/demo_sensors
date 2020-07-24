using System.Threading;

using NLog;

namespace AlexSandros.SensorsDemo.App.Reporting
{
    class UpdateSensorDataReport
    {
        private int _updatedSensors;

        private int _timedOutSensors;

        static private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void LogState(bool updateStopped = false)
        {
            int updatedSensors = Interlocked.Exchange(ref _updatedSensors, 0);
            int timedOutSensors = Interlocked.Exchange(ref _timedOutSensors, 0);
            // Trace использован для простоты - чтобы не конфигурировать цвета в NLog.config.
            if (updateStopped)
            {
                _logger.Trace($"Updated sensors: {updatedSensors}. Timed out sensors: {timedOutSensors}. Update stopped.");
            }
            else
            {
                _logger.Trace($"Updated sensors: {updatedSensors}. Timed out sensors: {timedOutSensors}. Starting new sensors data update.");
            }
        }

        public void IncrementUpdated() => Interlocked.Increment(ref _updatedSensors);

        public void IncrementTimedOut() => Interlocked.Increment(ref _timedOutSensors);
    }
}
