using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Business.Abstract;
using Core.Utilities.Business.Concrete;
using Core.Utilities.Interceptors;
using EC.Services.Order.Application.Data.Abstract.Dapper;
using EC.Services.Order.Application.Data.Concrete.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace EC.Services.Order.Application.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Services - AddScoped
            builder.RegisterType<SharedIdentityService>().As<ISharedIdentityService>().InstancePerLifetimeScope();

            #endregion
            #region DataAccess - AddTransient
            builder.RegisterType<DapperOrderRepository>().As<IDapperOrderRepository>().InstancePerDependency();

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
