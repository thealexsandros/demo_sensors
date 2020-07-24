using System;

namespace AlexSandros.SensorsDemo.App.Demo
{
    class Configuration
    {
        public const int SensorCount = 10000;

        public const int MinRequests = 10;

        public const int MaxRequests = 1000000;

        public const int StepRepeatCount = 5;

        static public readonly Func<int, int> GetNextStepRequests = x => x * 10;

        static public readonly TimeSpan StepLength = TimeSpan.FromSeconds(1);

        static public readonly TimeSpan RefreshInterval = TimeSpan.FromSeconds(1);

        static public readonly TimeSpan MaxExpectedSensorCallbackInterval = TimeSpan.FromSeconds(1.1);
    }
}
