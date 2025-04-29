using API.ENUMS;

namespace API.ReturnCodes.SuccessCodes
{
    public class SuccessCodes
    {
        public static readonly ReturnCode Success = new(200, "Operation completed successfully.");
        public static readonly ReturnCode Created = new(201, "Resource created successfully.");
        public static readonly ReturnCode NoContent = new(204, "No content to return.");
    }
}
