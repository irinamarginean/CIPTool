using MimeKit;

namespace BusinessLogicLayer.Utils
{
    public class EmailNotifier
    {
        public static void SendEmail(MimeMessage messageToSend)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {

                client.Connect("rb-smtp-int.bosch.com", 25, false);
                client.Send(messageToSend);
                client.Disconnect(true);
            }
        }
    }
}
