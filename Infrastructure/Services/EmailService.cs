using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private void Send(string email, string body)
    {
        var host = _configuration.GetSection("EmailConfig:Host").Value;
        var port = Int32.Parse(_configuration.GetSection("EmailConfig:Port").Value);
        var emailFrom = _configuration.GetSection("EmailConfig:Email").Value;
        var username = _configuration.GetSection("EmailConfig:Username").Value;
        var password = _configuration.GetSection("EmailConfig:Password").Value;
        var topic = _configuration.GetSection("EmailConfig:Topic").Value;

        var emailMessage = new MailMessage();

        emailMessage.From = new MailAddress(emailFrom);
        emailMessage.To.Add(new MailAddress(email));
        emailMessage.Subject = topic;
        emailMessage.Body = body;

        using (var client = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = true
        })
        {
            try
            {
                client.Send(emailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Dispose();
            }
        }
        return;
    }

    public void SendEmailConfirmation(string email, string confirmationPath)
    {
        var body = "To confirm email go via the link: " + confirmationPath;
        Send(email, body);
    }

    public void SendCredentials(string email, string password)
    {
        var body = "Password: " + password;
        Send(email, body);
    }
}
