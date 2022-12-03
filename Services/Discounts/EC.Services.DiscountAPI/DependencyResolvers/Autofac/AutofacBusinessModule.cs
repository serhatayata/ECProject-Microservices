using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.DataAccess.Queue;
using Core.Utilities.Interceptors;
using EC.Services.DiscountAPI.Consumers;
using EC.Services.DiscountAPI.Data.Abstract;
using EC.Services.DiscountAPI.Data.Concrete;
using EC.Services.DiscountAPI.Repositories.Abstract;
using EC.Services.DiscountAPI.Repositories.Concrete;
using MassTransit;
using System.Reflection;
using Module = Autofac.Module;

namespace EC.Services.DiscountAPI.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Services - AddScoped
            builder.RegisterType<DiscountRepository>().As<IDiscountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CampaignRepository>().As<ICampaignRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RedisCacheManager>().As<IRedisCacheManager>().InstancePerLifetimeScope();
            builder.RegisterType<ElasticSearchLogManager>().As<IElasticSearchLogService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductDeletedEventConsumer>().InstancePerLifetimeScope();

            #endregion
            #region DataAccess - AddTransient

            #endregion
            #region DbContext
            builder.RegisterType<DiscountContext>().As<IDiscountContext>();
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
