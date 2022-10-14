namespace IoMI.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true);
    Task SendEmailConfirmationTokenAsync(string to, string userId, string token);
    Task SendResetPasswordTokenAsync(string to, string userId, string token);
}