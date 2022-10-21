using IoMI.Application.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace IoMI.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        MailMessage mail = new()
        {
            IsBodyHtml = isBodyHtml,
            Subject = subject,
            Body = body,
            From = new MailAddress(_configuration["Email:EmailAddress"], "IoMI System", Encoding.UTF8)
        };

        mail.To.Add(to);

        SmtpClient smtpClient = new()
        {
            Host = _configuration["Email:Host"],
            Port = Convert.ToInt32(_configuration["Email:Port"]),
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_configuration["Email:EmailAddress"], _configuration["Email:Password"])
        };

        await smtpClient.SendMailAsync(mail);
    }

    public async Task SendEmailConfirmationTokenAsync(string to, string userId, string token)
    {
        StringBuilder mailBody = new();

        mailBody.Append("<div style=\"display: flex; flex-direction: column; box-sizing; border-box; padding: 32px; font-family: Trebuchet MS, Verdana; font-size: 18px;\">");
        mailBody.Append("<div style=\"display: flex; background-color: darkslategray; padding: 16px; border-radius: 8px; box-shadow: 0px 0px 8px gray; color: white; text-align: center; flex-direction: column; align-items: center;\">");
        mailBody.Append("<h3 style=\"margin: 8px 0;\">Hello,</h3><p>Welcome to IoMI!</p><p></p>");
        mailBody.Append("<p>We have just received your registration request.</p><p>Click the link below to confirm your email.</p><a target=\"blank\" href=\"");
        mailBody.Append(_configuration["ClientUrl"]);
        mailBody.Append($"user/confirmemail/{userId}/{token}\" ");
        mailBody.Append("style=\"width: 50%; align-self: center; text-align: center; padding: 8px 4px; border: 1px solid white; border-radius: 4px; text-decoration: none; cursor: pointer; background: rgba(255,255,255,.2); color: white;\"> Confirm Email </a>");
        mailBody.Append("<p></p><p>If this request is not yours, you do not need to do anything.</p><p></p><p><em>-IoMI Developer Team</em></p></div></div>");

        await SendEmailAsync(to, "IoMI - Email Confirmation", mailBody.ToString());
    }

    public async Task SendResetPasswordTokenAsync(string to, string userId, string token)
    {
        StringBuilder mailBody = new();

        mailBody.Append("<div style=\"display: flex; flex-direction: column; box-sizing; border-box; padding: 32px; font-family: Trebuchet MS, Verdana; font-size: 18px;\">");
        mailBody.Append("<div style=\"display: flex; flex-direction: column; background-color: darkslategray; padding: 16px; border-radius: 8px; box-shadow: 0px 0px 8px gray; color: white; text-align: center;\">");
        mailBody.Append("<h3 style=\"margin: 8px 0;\">Hello,</h3><p>We have received a request to reset password of your account.</p><p></p>");
        mailBody.Append("<p>No changes have been made to your account yet.</p><p>You can reset your password by clicking the link below:</p><a target=\"blank\" href=\"");
        mailBody.Append(_configuration["ClientUrl"]);
        mailBody.Append($"user/confirmnewpassword/{userId}/{token}\"");
        mailBody.Append("style=\"width: 50%; align-self: center; text-align: center; padding: 8px 4px; border: 1px solid white; border-radius: 4px; text-decoration: none; cursor: pointer; background: rgba(255,255,255,.2); color: white;\"> Reset Password </a>");
        mailBody.Append("<p></p><p> If you did not request a new password, you can safely ignore this email.</p><p></p><p><em>-IoMI Developer Team</em></p></div></div>");

        await SendEmailAsync(to, "IoMI - Reset Password Request", mailBody.ToString());
    }
}