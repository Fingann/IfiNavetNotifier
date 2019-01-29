using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace IfiNavetNotifier
{
    public class MailClient
    {
        public MailClient()
        {
        }

        public void Send(List<IfiEvent> ifiEvents, List<string> emails)
        {
        


            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("fingann92@gmail.com", "IFI BOT");
            var subject = $"Ifi {ifiEvents.Count} events updated";

            //var to = new EmailAddress("test@example.com", "Example User");

            //body
            string body = string.Empty;
            foreach (var ifiEvent in ifiEvents)
            {
                body += ifiEvent + Environment.NewLine;
            }
            var plainTextContent = body;
            foreach (var email in emails)
            {
                var msg = MailHelper.CreateSingleEmail(from, new EmailAddress(email), subject, plainTextContent,null);
                client.SendEmailAsync(msg).Wait();
            }





        }


}

}

