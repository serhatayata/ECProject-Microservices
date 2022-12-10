using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.Utilities.Interceptors;
using EC.Services.Communications.Services.Abstract;
using EC.Services.Communications.Services.Concrete;
using global::Autofac;
using global::Autofac.Extras.DynamicProxy;
using System.Reflection;
using Module = Autofac.Module;

namespace EC.Services.Communications.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region AddScoped
            //builder.RegisterType<SharedIdentityService>().As<ISharedIdentityService>().InstancePerLifetimeScope();
            #endregion
            #region AddTransient
            //builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().InstancePerDependency();
            #endregion
            #region Singleton
            builder.RegisterType<EmailService>().As<IEmailService>().SingleInstance();
            builder.RegisterType<ElasticSearchLogManager>().As<IElasticSearchLogService>().SingleInstance();
            #endregion

            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                  .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                  {
                      Selector = new AspectInterceptorSelector()
                  }).SingleInstance();
        }
    }
}
