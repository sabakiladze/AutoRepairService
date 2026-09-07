using AutoRepairService.Application.ServiceInterfaces;
using AutoRepairService.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AutoRepairService.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendVerificationEmailAsync(string email, string token)
        {
            using SmtpClient smtp = new(_emailSettings.SmtpServer)
            {
                Port = _emailSettings.Port,
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    _emailSettings.SenderEmail,
                    _emailSettings.AppPassword)
            };

            using MailMessage message = new()
            {
                From = new MailAddress(
                    _emailSettings.SenderEmail,
                    _emailSettings.SenderName),

                Subject = "Email Verification",
                IsBodyHtml = true
            };

            message.To.Add(email);
            message.Body = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f7f6;
            margin: 0;
            padding: 0;
            color: #333333;
        }}

        .email-wrapper {{
            width: 100%;
            background-color: #f4f7f6;
            padding: 40px 0;
        }}

        .email-content {{
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
            border-radius: 8px;
            overflow: hidden;
        }}

        .email-header {{
            background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%);
            color: #ffffff;
            text-align: center;
            padding: 30px 20px;
        }}

        .email-header h1 {{
            margin: 0;
            font-size: 24px;
        }}

        .email-body {{
            padding: 40px 30px;
            text-align: center;
        }}

        .email-body p {{
            font-size: 16px;
            line-height: 1.6;
            color: #555555;
        }}

        .verify-button {{
            display: inline-block;
            background-color: #007bff;
            color: #ffffff !important;
            text-decoration: none;
            padding: 14px 32px;
            font-size: 16px;
            font-weight: 600;
            border-radius: 6px;
        }}

        .email-footer {{
            background-color: #f8f9fa;
            text-align: center;
            padding: 20px;
            font-size: 12px;
            color: #888888;
        }}
    </style>
</head>

<body>
    <table class='email-wrapper' role='presentation' cellpadding='0' cellspacing='0'>
        <tr>
            <td align='center'>
                <table class='email-content' role='presentation' cellpadding='0' cellspacing='0'>

                    <tr>
                        <td class='email-header'>
                            <h1>Auto Repair System</h1>
                        </td>
                    </tr>

                    <tr>
                        <td class='email-body'>
                            <p>
                                Welcome! Thank you for registering.
                                Please verify your email address:
                            </p>

                            <a href='https://localhost:5001/api/authentication/verify?token={token}'
                               class='verify-button'
                               target='_blank'>
                                Verify Email
                            </a>
                        </td>
                    </tr>

                    <tr>
                        <td class='email-footer'>
                            If you received this message by mistake,
                            please ignore it.
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</body>
</html>";


            await smtp.SendMailAsync(message);
        }
    }
}