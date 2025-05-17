namespace Aton_ITTP.Utils
{
    /// <summary>
    /// Базовый класс для возврата результата операции
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Флаг успешности выполнения операции
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Сообщение об ошибке (если операция не выполнена)
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Конструктор для создания результата
        /// </summary>
        /// <param name="success">Флаг успешности</param>
        /// <param name="error">Сообщение об ошибке</param>
        protected Result(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        /// <summary>
        /// Успешный результат
        /// </summary>
        public static Result WithData()
        {
            return new Result(true, null);
        }

        /// <summary>
        /// Неуспешный результат с сообщением об ошибке
        /// </summary>
        /// <param name="error">Сообщение об ошибке</param>
        public static Result WithError(string error)
        {
            return new Result(false, error);
        }
    }
}
