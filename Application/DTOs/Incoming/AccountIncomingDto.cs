namespace Application.DTOs.Incoming
{
    public class AccountIncomingDto
    {
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReEnteredPassword { get; set; }
    }
}
