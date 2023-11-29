namespace SCAGEScale.Application.DTO
{
    public class ResponseDto
    {
        public string Message { get; set; }
        public object Data { get; set; }

        public static string Error(string message)
        {
            return message;
        }

        public static ResponseDto New(string message, object data)
        {
            var response = new ResponseDto
            {
                Message = message,
                Data = data
            };
            return response;
        }
    }
}
