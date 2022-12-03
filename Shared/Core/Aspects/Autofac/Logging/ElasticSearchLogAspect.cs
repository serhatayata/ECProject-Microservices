using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.DataAccess.Redis;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Logging
{
    public class ElasticSearchLogAspect : MethodInterception
    {
        private static byte _risk;
        private readonly IElasticSearchLogService _client;

        public ElasticSearchLogAspect(byte risk)
        {
            _risk = risk;
            _client = ServiceTool.ServiceProvider.GetService<IElasticSearchLogService>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (var i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name
                });
            }

            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.ReflectedType.FullName,
                LogParameters = logParameters,
                Risk = _risk,
                LoggingTime = DateTime.Now.ToString()
            };

            Task.Run(() =>
            {
                _client.AddAsync(logDetail);
            });
            invocation.Proceed();
        }
    }
}