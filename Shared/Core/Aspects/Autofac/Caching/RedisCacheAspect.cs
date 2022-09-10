using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.DataAccess.Redis;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Core.Utilities.Results;
using Core.Extensions;
using AspectCore.DynamicProxy;
using System.Runtime.CompilerServices;

namespace Core.Aspects.Autofac.Caching
{
    public class RedisCacheAspect<T> : MethodInterception where T : class
    {
        private int _duration;
        private IRedisCacheManager _cacheManager;

        public RedisCacheAspect(int duration = 60) //Default 60 dakika verdik.
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<IRedisCacheManager>();
        }

        public async override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.Name}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            //Bu şkilde kullandığımda class parametrelerini algılamıyor. Key olarak sadece methodName ini alıyor. Dolayısı ile parametreler değişse de Cash'i yenilemiyor.
            //var key = $"{methodName}({string.Join(",", arguments.Select(x=>x?.ToString()??"<Null>"))})";            
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x != null ? JsonConvert.SerializeObject(x) : "<Null>"))})";

            if (_cacheManager.KeyExists(key))//Çağırılan method ve parametresi cache de varsa oradan getir.
            {
                var cacheValue = _cacheManager.Get(key);
                var data = JsonConvert.DeserializeObject<T>(cacheValue, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                var isAsync = invocation.Method.IsReturnTask();
                //bool isAsync = invocation.Method.ReturnType.Name.Contains("Task");

                if (isAsync)
                {
                    var returnValue = Task.FromResult(data);
                    invocation.ReturnValue = returnValue;
                }
                else
                    invocation.ReturnValue = data;

                return;
            }

            invocation.Proceed();

            var method = invocation.MethodInvocationTarget;
            var isAsync2 = method.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null;
            if (isAsync2 && typeof(Task).IsAssignableFrom(method.ReturnType))
            {
                var cacheValue = await InterceptAsync((dynamic)invocation.ReturnValue);
                _cacheManager.Set(key, cacheValue, _duration);
            }
            else
                _cacheManager.Set(key, invocation.ReturnValue, _duration);
        }

        private static async Task InterceptAsync(Task task)
        {
            await task.ConfigureAwait(false);
            // do the continuation work for Task...
        }

        private static async Task<T> InterceptAsync<T>(Task<T> task)
        {
            T result = await task.ConfigureAwait(false);
            // do the continuation work for Task<T>...
            return result;
        }
    }
}
