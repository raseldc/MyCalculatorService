using System.Net.Mail;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
namespace WebCalculator.services
{
    public class EmailService : IEmailService
    {
        public void SendEmailForPassword(string email,String password)
        {
            string to = email; //To address    
            string from = "shamiulgt@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Use this password for login "+password;
            message.Subject = "Sending From Calculator Application";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("shamiulgt@gmail.com", "qixd yqho brak ksnm");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}