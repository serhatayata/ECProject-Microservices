namespace EC.Services.Communications.Services.Abstract
{
    public interface IEmailSmtpConsumerService
    {
        Task ConsumeEmailActivationSmtpEmail();
    }
}
