namespace TaktTusur.Media.Clients.VkApi.Models
{
    public class VkApiException: Exception
    {
        public VkApiException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Код ошибки.
        /// </summary>
        public int ErrorCode {get; set;}
    }
}