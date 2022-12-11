using Autofac.Core;
using Core.CrossCuttingConcerns.Communication.MessageQueue.Abstract;
using Core.Entities;
using EC.Services.Communications.Services.Abstract;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EC.Services.Communications.Services.Concrete
{
    public class EmailSmtpConsumerService : IEmailSmtpConsumerService
    {
        public Task ConsumeEmailActivationSmtpEmail()
        {
            throw new NotImplementedException();
        }
    }
}
