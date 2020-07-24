using System;

namespace AlexSandros.SensorsDemo.App.Contracts.Public
{
    /// <summary>
    /// Результат получения данных комплекта датчиков.
    /// </summary>
    public class GetSensorDataResult
    {
        /// <summary>
        /// Возвращает или устанавливает параметры операции.
        /// </summary>
        public GetSensorDataParameters Parameters { get; set; }

        /// <summary>
        /// Возвращает или устанавливает данные комплекта датчиков.
        /// </summary>
        public SensorData SensorData { get; set; }

        /// <summary>
        /// Возвращает или устанавливает время последнего обновления данных с комплекта датчиков в часовом поясе UTC.
        /// Eсли не удалось получить данные, время не будет заполнено.
        /// </summary>
        public DateTime? SensorDataLastUpdateDateTimeUtc { get; set; }

        /// <summary>
        /// Возвращает или устанавливает значение, указывающее, что передан некорректный идентификатор комлекта датчиков.
        /// </summary>
        public bool InvalidSensorId { get; set; }

        /// <summary>
        /// Возвращает или устанавливает значение, указывающее, что с комлекта датчиков не получены данные.
        /// </summary>
        public bool NoDataFromSensor { get; set; }
    }
}
