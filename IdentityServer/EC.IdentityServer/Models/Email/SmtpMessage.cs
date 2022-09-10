
namespace EC.IdentityServer.Models.Email
{
    public class SmtpMessage
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public SmtpMessage(List<string> to, string subject, string content)
        {
            To = to;
            Subject = subject;
            Content = content;
        }

    }
}
