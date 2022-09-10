using Core.Entities;

namespace EC.IdentityServer.Models.Settings
{
    public class SmtpSetting:ISetting
    {
        public string SmtpClient { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }

    }
}
