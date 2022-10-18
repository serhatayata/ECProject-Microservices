using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.Utilities.Business.Abstract;
using Core.Utilities.Business.Concrete;
using Core.Utilities.Interceptors;
using EC.Services.PaymentAPI.ApiServices.Abstract;
using EC.Services.PaymentAPI.ApiServices.Concrete;
using EC.Services.PaymentAPI.Data.Abstract.Dapper;
using EC.Services.PaymentAPI.Data.Concrete.Dapper;
using EC.Services.PaymentAPI.Services.Abstract;
using EC.Services.PaymentAPI.Services.Concrete;
using System.Reflection;
using Module = Autofac.Module;

namespace EC.Services.PaymentAPI.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Services - AddScoped
            //builder.RegisterType<CategoryManager>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentManager>().As<IPaymentService>().InstancePerLifetimeScope();
            builder.RegisterType<RedisCacheManager>().As<IRedisCacheManager>().InstancePerLifetimeScope();
            builder.RegisterType<ElasticSearchManager>().As<IElasticSearchService>().InstancePerLifetimeScope();
            builder.RegisterType<SharedIdentityService>().As<ISharedIdentityService>().InstancePerLifetimeScope();

            #endregion
            #region DataAccess - AddTransient
            builder.RegisterType<ProductApiService>().As<IProductApiService>().InstancePerDependency();
            builder.RegisterType<DiscountApiService>().As<IDiscountApiService>().InstancePerDependency();
            builder.RegisterType<DapperPaymentRepository>().As<IDapperPaymentRepository>().InstancePerDependency();

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
