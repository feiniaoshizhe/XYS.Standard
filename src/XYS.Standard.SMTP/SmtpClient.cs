using MailKit.Net.Smtp;

namespace XYS.Standard.SMTP
{
    public class SmtpClient<T>  where T : SmtpServer, new()
    {
        public static SmtpClient GetSmtpClient()
        {
            var client = new SmtpClient();
            client.ConnectAsync();
            client.AuthenticateAsync();
            return client;
        }
    }
}
