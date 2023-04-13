namespace Application.Interfaces
{
    public interface IEmailService
    {
        void SendEmailConfirmation(string email, string confirmationPath);
        void SendCredentials(string email, string password);
    }
}