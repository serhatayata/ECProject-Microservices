using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Communication.RabbitMQClientServices;
using Core.Utilities.Interceptors;
using EC.IdentityServer.Publishers;
using EC.IdentityServer.Services.Abstract;
using EC.IdentityServer.Services.Concrete;
using System.Reflection;
using Module = Autofac.Module;

namespace EC.IdentityServer.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<RedisCacheManager>().As<IRedisCacheManager>().InstancePerDependency();
            builder.RegisterType<RabbitMQPublisher>().SingleInstance();
            builder.RegisterType<EmailSmtpClientService>().SingleInstance();

            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                  .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                  {
                      Selector = new AspectInterceptorSelector()
                  }).SingleInstance();
        }

    }
}
