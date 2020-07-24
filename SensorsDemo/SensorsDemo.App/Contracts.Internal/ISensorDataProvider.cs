using System.Threading.Tasks;

using AlexSandros.SensorsDemo.App.Contracts.Public;

namespace AlexSandros.SensorsDemo.App.Contracts.Internal
{
    /// <summary>
    /// Интерфейс поставщика данных с комплектов датчиков.
    /// </summary>
    public interface ISensorDataProvider
    {
        /// <summary>
        /// Возвращает данные с комплекта датчиков по идентификатору.
        /// </summary>
        Task<SensorData> GetSensorDataAsync(int sensorId);
    }
}