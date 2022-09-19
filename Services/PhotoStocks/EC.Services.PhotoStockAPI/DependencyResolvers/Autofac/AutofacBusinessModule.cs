using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.DataAccess.Dapper;
using Core.Utilities.Interceptors;
using EC.Services.PhotoStockAPI.Data.Abstract;
using EC.Services.PhotoStockAPI.Data.Concrete;
using EC.Services.PhotoStockAPI.Services.Abstract;
using EC.Services.PhotoStockAPI.Services.Concrete;
using System.Reflection;
using Module = Autofac.Module;

namespace EC.Services.PhotoStockAPI.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule: Module
    {
        private readonly IConfiguration _configuration;
        private readonly string _connString;
        public AutofacBusinessModule(IConfiguration configuration)
        {
            _configuration = configuration;
            _connString = _configuration.GetConnectionString("DefaultConnection");
        }

        protected override void Load(ContainerBuilder builder)
        {
            #region Services - AddScoped
            builder.RegisterType<PhotoManager>().As<IPhotoService>().InstancePerLifetimeScope();
            builder.RegisterType<RedisCacheManager>().As<IRedisCacheManager>().InstancePerLifetimeScope();
            #endregion
            #region DataAccess - AddTransient
            builder.RegisterType<PhotoRepository>().As<IPhotoRepository>().InstancePerDependency();
            builder.RegisterType<DapperManager>().As<IDapperManager>().InstancePerDependency().WithParameter("connectionString", _connString);

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
