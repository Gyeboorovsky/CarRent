namespace CarRent.Service.Services.EmailSender;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);

    Task Execute(string apiKey, string subject, string message, string email);
}