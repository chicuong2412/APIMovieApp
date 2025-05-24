namespace API.Models
{
    public class PasswordResetCode
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public DateTime ExpiredDate { get; set; }

        public bool IsOpenToChange { get; set; } = false;

        public DateTime? ExpiredChangePasswor {  get; set; }

        public string? Token { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
