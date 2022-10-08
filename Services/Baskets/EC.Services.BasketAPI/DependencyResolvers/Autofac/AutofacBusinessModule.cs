using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Business.Abstract;
using Core.Utilities.Business.Concrete;
using Core.Utilities.Interceptors;
using EC.Services.BasketAPI.Services.Abstract;
using EC.Services.BasketAPI.Services.Concrete;
using System.Reflection;
using Module = Autofac.Module;

namespace EC.Services.BasketAPI.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Services - AddScoped
            builder.RegisterType<BasketService>().As<IBasketService>().InstancePerLifetimeScope();
            builder.RegisterType<SharedIdentityService>().As<ISharedIdentityService>().InstancePerLifetimeScope();
            #endregion
            #region Consumer - AddScoped
            //builder.RegisterType<ProductDeletedEventConsumer>().InstancePerLifetimeScope();
            #endregion

            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                  .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                  {
                      Selector = new AspectInterceptorSelector()
                  }).SingleInstance();

            //AddTransient
            //builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().InstancePerDependency();
        }
    }
}
