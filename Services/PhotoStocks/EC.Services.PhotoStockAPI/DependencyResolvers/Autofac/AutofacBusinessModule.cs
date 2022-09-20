using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.DataAccess.Dapper;
using Core.Utilities.Interceptors;
using EC.Services.PhotoStockAPI.Data.Abstract.Dapper;
using EC.Services.PhotoStockAPI.Data.Abstract.EntityFramework;
using EC.Services.PhotoStockAPI.Data.Concrete.Dapper;
using EC.Services.PhotoStockAPI.Data.Concrete.EntityFramework;
using EC.Services.PhotoStockAPI.Services.Abstract;
using EC.Services.PhotoStockAPI.Services.Concrete;
using System.Reflection;
using Module = Autofac.Module;

namespace EC.Services.PhotoStockAPI.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Services - AddScoped
            builder.RegisterType<PhotoManager>().As<IPhotoService>().InstancePerLifetimeScope();
            builder.RegisterType<RedisCacheManager>().As<IRedisCacheManager>().InstancePerLifetimeScope();
            #endregion
            #region DataAccess - AddTransient
            builder.RegisterType<DapperPhotoRepository>().As<IDapperPhotoRepository>().InstancePerDependency();
            builder.RegisterType<EfPhotoRepository>().As<IEfPhotoRepository>().InstancePerDependency();

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
