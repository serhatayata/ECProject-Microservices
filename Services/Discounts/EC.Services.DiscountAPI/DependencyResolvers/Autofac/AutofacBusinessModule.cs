using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.Utilities.Interceptors;
using EC.Services.DiscountAPI.Data.Abstract.Dapper;
using EC.Services.DiscountAPI.Data.Abstract.EntityFramework;
using EC.Services.DiscountAPI.Data.Concrete.Dapper;
using EC.Services.DiscountAPI.Data.Concrete.EntityFramework;
using EC.Services.DiscountAPI.Services.Abstract;
using EC.Services.DiscountAPI.Services.Concrete;
using System.Reflection;
using Module = Autofac.Module;

namespace EC.Services.DiscountAPI.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Services - AddScoped
            builder.RegisterType<DiscountManager>().As<IDiscountService>().InstancePerLifetimeScope();
            builder.RegisterType<RedisCacheManager>().As<IRedisCacheManager>().InstancePerLifetimeScope();
            builder.RegisterType<ElasticSearchManager>().As<IElasticSearchService>().InstancePerLifetimeScope();
            #endregion
            #region DataAccess - AddTransient
            builder.RegisterType<DapperDiscountRepository>().As<IDapperDiscountRepository>().InstancePerDependency();
            builder.RegisterType<EfDiscountRepository>().As<IEfDiscountRepository>().InstancePerDependency();

            #endregion


            //AddTransient
            //builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().InstancePerDependency();

            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                  .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                  {
                      Selector = new AspectInterceptorSelector()
                  }).SingleInstance();
        }
    }
}
