using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using CustomTracker.Models.Abstract;

namespace CustomTracker.Models.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "customer@example.com";
        public string MailFromAddress = "dmitriybuga@mail.ru";
        public bool UseSsl = true;
        public string Username = "dmitriybuga";
        public string Password = "ssssss";
        public string ServerName = "smtp.mail.ru";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\QueryTracker";
    }
    public class EmailQueryProcessor : IQueryProcessor
    {
        private EmailSettings emailSettings;
        public EmailQueryProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessQuery(Tickets query, string body)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                if (body == null)
                {
                    StringBuilder bodyMail = new StringBuilder()
                  .AppendLine("The number of your query is:" + query.Reference)
                  .AppendLine("---")
                  .AppendLine("Query");
                    bodyMail.AppendLine(query.Subject);
                    bodyMail.AppendLine(query.Body);
                    body = bodyMail.ToString();
                }
                emailSettings.MailToAddress = query.Email;
                MailMessage mailMessage = new MailMessage(
                  emailSettings.MailFromAddress, // From
                  emailSettings.MailToAddress, // To
                  "New order submitted!", // Subject
                  body.ToString()); // Body

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
            }
        }
    }
}