using API.ENUMS;

namespace API.DTOs
{
    public class APIresponse<T>
    {
        public int code {  get; set; }

        public string message { get; set; }

        public APIresponse(ReturnCode code)
        {
            this.code = code.Code;
            this.message = code.Message;
        }

        public APIresponse()
        {
            
        }

        public T data { get; set; }
    }
}
