using BusinessLogicLayer.Utils;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;
using AspNetCore.Email;

namespace CIPTool.Helpers
{
    public class EmailSender : IEmailSender
    {
        public async Task<bool> SendEmailAsync(EmailDto input)
        {
            return true;
        }

        public async Task<bool> SendEmailAsync(string recipient, string subject, string htmlMessage)
        {
            var messageToSend = new MimeMessage
            {
                Sender = new MailboxAddress("Support ECC CIP Tool", "Support.ECCCIPTool@ro.bosch.com"),
                Subject = subject
            };
            messageToSend.To.Add(address: new MailboxAddress(recipient));

            messageToSend.Body = new TextPart(TextFormat.Html)
            {
                Text = htmlMessage
            };

            EmailNotifier.SendEmail(messageToSend);

            return true;
        }
    }
}
