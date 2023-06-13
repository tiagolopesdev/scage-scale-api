

namespace SCAGEScale.Application.Utils
{
    public class RequestResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }

        public static string Error(string message)
        {
            return message;
        }

        public static RequestResponse New(string message, object data)
        {
            var response = new RequestResponse
            {
                Message = message,
                Data = data
            };
            return response;
        }
    }
}
