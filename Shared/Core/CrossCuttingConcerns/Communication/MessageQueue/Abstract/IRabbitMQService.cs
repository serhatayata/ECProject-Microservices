using Core.Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Communication.MessageQueue.Abstract
{
    public interface IRabbitMQService
    {
        void SendEmailActivationSmtpEmail(EmailData model);

    }
}
