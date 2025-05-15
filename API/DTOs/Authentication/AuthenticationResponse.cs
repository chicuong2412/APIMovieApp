namespace API.DTOs.Authentication
{
    public class AuthenticationResponse
    {
        public string? UserName { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public string? Token { get; set; } = string.Empty;

        public string? RefreshToken { get; set; } = string.Empty;

        public int? TokenExpiry { get; set; } = 0;
    }
}
