namespace AlexSandros.SensorsDemo.App.Contracts.Public
{
    public interface ISensorService
    {
        /// <summary>
        /// Возвращает данные комплекта датчиков.
        /// </summary>
        GetSensorDataResult GetSensorData(GetSensorDataParameters parameters);
    }
}
