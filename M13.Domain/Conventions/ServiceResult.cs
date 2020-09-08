namespace M13.Domain.Conventions
{
    public class ServiceResult
    {
        public ServiceResult(bool isOk, string errorMessage = "")
        {
            IsOk = isOk;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Успешность выполнения сервиса
        /// </summary>
        public bool IsOk { get; }

        /// <summary>
        /// Сообщения об ошибке
        /// </summary>
        public string ErrorMessage { get; }
        
        public static ServiceResult Success => new ServiceResult(true);
        public static ServiceResult Fail => new ServiceResult(false);
    }
}