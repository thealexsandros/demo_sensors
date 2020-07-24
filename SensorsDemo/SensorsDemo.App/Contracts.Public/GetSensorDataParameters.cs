namespace AlexSandros.SensorsDemo.App.Contracts.Public
{
    /// <summary>
    /// Параметры получения данных комплекта датчиков.
    /// </summary>
    public class GetSensorDataParameters
    {
        /// <summary>
        /// Возвращает или устанавливает идентификатор комплекта датчиков.
        /// </summary>
        public int SensorId { get; set; }
    }
}
