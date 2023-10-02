using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Util.Store;
using StockQuote.Domain.Interfaces.Services;
using System.Net;
using System.Net.Mail;

namespace StockQuote.Infrastructure.Services
{
    public class EmailSenderService : IEmailSenderService
    {

        private readonly string? _smtpServer;
        private readonly int _smtpPort;
        private readonly string? _smtpUsername;
        private readonly string? _smtpPassword;

        public EmailSenderService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {

                var credential = await GetCredential();

                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                    client.EnableSsl = true;

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(_smtpUsername);
                    mail.To.Add(new MailAddress(toEmail));
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    await client.SendMailAsync(mail);

                    Console.WriteLine("E-mail enviado com sucesso.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar o e-mail: " + ex.Message);
            }
        }

        private static async Task<UserCredential> GetCredential()
        {
            UserCredential credential;

            using (var stream = new System.IO.FileStream("credentials.json", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = "ClientId",
                        ClientSecret = "ClientSecret"
                    },
                    new[] { GmailService.Scope.GmailSend },
                    "user",
                    System.Threading.CancellationToken.None,
                    new FileDataStore("Gmail.Send.Sender"));
            }

            return credential;
        }
    }
}
