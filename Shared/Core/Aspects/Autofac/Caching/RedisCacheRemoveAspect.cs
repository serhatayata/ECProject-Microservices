using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class RedisCacheRemoveAspect:MethodInterception
    {
        private string _pattern;
        private IRedisCacheManager _cacheManager;

        public RedisCacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<IRedisCacheManager>();
        }

        //Veritabanında birşey değiştiğinde, mesela yeni ürün eklendiğinde cache'i siliyoruz.
        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
