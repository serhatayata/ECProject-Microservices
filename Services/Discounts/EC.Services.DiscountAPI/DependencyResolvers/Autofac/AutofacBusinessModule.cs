using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.DataAccess.Queue;
using Core.Utilities.Interceptors;
using EC.Services.DiscountAPI.Consumers;
using EC.Services.DiscountAPI.Data.Abstract.Dapper;
using EC.Services.DiscountAPI.Data.Abstract.EntityFramework;
using EC.Services.DiscountAPI.Data.Concrete;
using EC.Services.DiscountAPI.Data.Concrete.Dapper;
using EC.Services.DiscountAPI.Data.Concrete.EntityFramework;
using EC.Services.DiscountAPI.Data.Contexts;
using EC.Services.DiscountAPI.Services.Abstract;
using EC.Services.DiscountAPI.Services.Concrete;
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
            builder.RegisterType<CampaignService>().As<ICampaignService>().InstancePerLifetimeScope();
            builder.RegisterType<DiscountService>().As<IDiscountService>().InstancePerLifetimeScope();

            builder.RegisterType<RedisCacheManager>().As<IRedisCacheManager>().InstancePerLifetimeScope();
            builder.RegisterType<ElasticSearchLogManager>().As<IElasticSearchLogService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductDeletedEventConsumer>().InstancePerLifetimeScope();

            #endregion
            #region DataAccess - AddTransient
            builder.RegisterType<EfDiscountDal>().As<IDiscountDal>().InstancePerDependency();
            builder.RegisterType<DiscountRepository>().As<IDiscountRepository>().InstancePerDependency();
            builder.RegisterType<EfCampaignDal>().As<ICampaignDal>().InstancePerDependency();
            builder.RegisterType<CampaignRepository>().As<ICampaignRepository>().InstancePerDependency();
            #endregion
            #region DbContext
            builder.RegisterType<DiscountDbDapperContext>().SingleInstance();
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
