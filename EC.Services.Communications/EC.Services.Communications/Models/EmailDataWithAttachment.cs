namespace EC.Services.Communications.Models
{
    public class EmailDataWithAttachment:EmailData
    {
        public IFormFileCollection EmailAttachments { get; set; }
    }
}
