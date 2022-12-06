using EC.Services.Communications.Models;
using EC.Services.Communications.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.Communications.Controllers
{
    [Route("communication/api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        IEmailService _emailService = null;
        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("send-smtp-email")]
        public async Task<bool> SendSmtpEmail(EmailData emailData)
        {
            return await _emailService.SendSmtpEmail(emailData);
        }
    }
}
