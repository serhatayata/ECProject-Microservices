using Autofac;
using EC.IdentityServer.Services.Abstract;
using EC.IdentityServer.Services.Concrete;
using System.IdentityModel.Tokens.Jwt;

namespace EC.IdentityServer.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerDependency();
            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerDependency();







        }

    }
}
