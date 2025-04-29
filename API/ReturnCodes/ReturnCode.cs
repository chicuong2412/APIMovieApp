namespace API.ENUMS
{
    public class ReturnCode
    {
        public int Code { get; }
        public string Message { get; }

        public ReturnCode(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
    
        
}
