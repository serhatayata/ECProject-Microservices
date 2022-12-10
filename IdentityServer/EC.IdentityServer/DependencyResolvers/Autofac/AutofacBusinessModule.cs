using Autofac;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Communication.MessageQueue.Abstract;
using Core.CrossCuttingConcerns.Communication.MessageQueue.Concrete;
using EC.IdentityServer.ApiServices.Abstract;
using EC.IdentityServer.ApiServices.Concrete;
using EC.IdentityServer.Services.Abstract;
using EC.IdentityServer.Services.Concrete;
using System.IdentityModel.Tokens.Jwt;

namespace EC.IdentityServer.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommunicationApiService>().As<ICommunicationApiService>().InstancePerDependency();
            builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<RedisCacheManager>().As<IRedisCacheManager>().InstancePerDependency();
            builder.RegisterType<RabbitMQService>().As<IRabbitMQService>().InstancePerDependency();


        }

    }
}
