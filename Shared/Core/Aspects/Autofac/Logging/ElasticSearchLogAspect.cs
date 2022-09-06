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
        private readonly IElasticSearchService _client;

        public ElasticSearchLogAspect(byte risk)
        {
            _risk = risk;
            _client = ServiceTool.ServiceProvider.GetService<IElasticSearchService>();
        }

        public override void Intercept(IInvocation invocation)
        {
            //var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            //var arguments = invocation.Arguments.ToList();
            //var key = KeyGenerator.GetCacheKey(invocation.Method, invocation.Arguments, "FoodThen");

            var logParameters = new List<LogParameter>();
            for (var i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i]?.GetType().Name
                });
            }

            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters,
                Risk = _risk,
                LoggingTime = DateTime.Now.ToString()
            };

            _client.Add(logDetail);
            invocation.Proceed();
        }
    }
}