using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.Utilities.Interceptors;
using EC.Services.CategoryAPI.Data.Abstract.Dapper;
using EC.Services.CategoryAPI.Data.Abstract.EntityFramework;
using EC.Services.CategoryAPI.Data.Concrete.Dapper;
using EC.Services.CategoryAPI.Data.Concrete.EntityFramework;
using EC.Services.CategoryAPI.Services.Abstract;
using EC.Services.CategoryAPI.Services.Concrete;
using System.Reflection;
using Module = Autofac.Module;

namespace EC.Services.CategoryAPI.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Services - AddScoped
            builder.RegisterType<CategoryManager>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<RedisCacheManager>().As<IRedisCacheManager>().InstancePerLifetimeScope();
            builder.RegisterType<ElasticSearchManager>().As<IElasticSearchService>().InstancePerLifetimeScope();
            #endregion
            #region DataAccess - AddTransient
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerDependency();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().InstancePerDependency();

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
