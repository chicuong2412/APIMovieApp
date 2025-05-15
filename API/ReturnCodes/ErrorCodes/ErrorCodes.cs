namespace API.ENUMS.ErrorCodes
{
    public class ErrorCodes
    {
        public static readonly ReturnCode EmailExisted = new(300, "Email already exists.");
        public static readonly ReturnCode UsernameExisted = new(301, "Email already exists.");

        public static readonly ReturnCode DataInvalid = new(400, "Your input data is invalid.");
        public static readonly ReturnCode Unauthorized = new(401, "Unauthorized access.");
        public static readonly ReturnCode NotFound = new(404, "Resource not found.");
        public static readonly ReturnCode Expired = new(405, "Expired.");
        public static readonly ReturnCode RequestSpam = new(429, "Too many request");
        public static readonly ReturnCode Conflict = new(409, "Conflict with the current state of the resource.");
        public static readonly ReturnCode ServerError = new(500, "An unexpected server error occurred.");
        public static readonly ReturnCode DataConflict = new(400, "Some of the data conflict with others.");
        public static readonly ReturnCode NoSeasons = new(400, "This movie doesn't have any seaons!!!");

        //// Custom Movie API Errors
        //public static readonly ReturnCode MovieNotFound = new(404, "The requested movie does not exist.");
        //public static readonly ReturnCode SeasonNotFound = new(404, "The requested season was not found.");
        //public static readonly ReturnCode VideoNotFound = new(404, "The requested video could not be located.");
        //public static readonly ReturnCode InvalidToken = new(401, "Invalid or expired token.");
        //public static readonly ReturnCode AccessDenied = new(403, "You don't have permission to access this resource.");
        //public static readonly ReturnCode ValidationError = new(422, "One or more validation errors occurred.");
    }
}
