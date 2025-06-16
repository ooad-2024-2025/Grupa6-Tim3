using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RealEstateHub.Services
{
    public class EmailHelper
    {
        public static async Task<bool> SendEmailAsync(string email, string subject, string htmlContent)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();

                message.From = new MailAddress("realestatehubapp@gmail.com");
                message.To.Add(email);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = htmlContent;

                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("realestatehubapp@gmail.com", "pbpzltuugkayvmjr");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                await smtpClient.SendMailAsync(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
