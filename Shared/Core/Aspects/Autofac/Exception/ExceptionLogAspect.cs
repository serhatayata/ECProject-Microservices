using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Entities;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private static byte _risk;
        private readonly IElasticSearchLogService _client;

        public ExceptionLogAspect(byte risk)
        {
            _risk = risk;
            _client = ServiceTool.ServiceProvider.GetService<IElasticSearchLogService>();
        }

        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            var logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = e.Message;

            Task.Run(() =>
            {
                _client.AddAsync(logDetailWithException);
            });
            invocation.Proceed();
        }
        private static LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name=invocation.GetConcreteMethod().GetParameters()[i].Name,
                });
            }

            var logDetailWithException = new LogDetailWithException
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters,
                Risk = _risk
            };

            return logDetailWithException;
        }
    }
}
