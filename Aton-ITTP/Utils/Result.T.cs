namespace Aton_ITTP.Utils
{
    /// <summary>
    /// Обобщенный класс для возврата результата операции с данными
    /// </summary>
    /// <typeparam name="T">Тип возвращаемых данных</typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// Возвращаемые данные
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Конструктор для создания результата с данными
        /// </summary>
        /// <param name="value">Возвращаемые данные</param>
        /// <param name="success">Флаг успешности</param>
        /// <param name="error">Сообщение об ошибке</param>
        protected Result(T value, bool success, string error) : base(success, error)
        {
            Value = value;
        }

        /// <summary>
        /// Успешный результат с данными
        /// </summary>
        /// <param name="value">Возвращаемые данные</param>
        public static Result<T> WithData(T value)
        {
            return new Result<T>(value, true, null);
        }

        /// <summary>
        /// Неуспешный результат с сообщением об ошибке
        /// </summary>
        /// <param name="error">Сообщение об ошибке</param>
        public static new Result<T> WithError(string error)
        {
            return new Result<T>(default, false, error);
        }
    }
}
