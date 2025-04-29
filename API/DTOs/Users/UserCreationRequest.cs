namespace API.DTOs.Users
{
    public class UserCreationRequest
    {

        public string Username { get; set; } = string.Empty.ToString();
        public string Password { get; set; } = string.Empty.ToString();
        public string Name { get; set; } = string.Empty.ToString();

        public int Age { get; set; } = 0;

        public string Email { get; set; } = string.Empty.ToString();

        public DateTime DoB { get; set; } = DateTime.Now;

        public String PhoneNumber { get; set; } = string.Empty.ToString();

        public string Address { get; set; } = string.Empty.ToString();
    }
}
