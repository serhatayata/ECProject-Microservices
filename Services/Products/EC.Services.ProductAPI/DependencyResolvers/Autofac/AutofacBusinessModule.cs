using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using EC.Services.ProductAPI.Data.Abstract;
using EC.Services.ProductAPI.Data.Concrete;
using EC.Services.ProductAPI.Repositories.Abstract;
using EC.Services.ProductAPI.Repositories.Concrete;
using System.Reflection;
using Module = Autofac.Module;

namespace EC.Services.ProductAPI.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Services - AddScoped
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductVariantRepository>().As<IProductVariantRepository>().InstancePerLifetimeScope();
            builder.RegisterType<VariantRepository>().As<IVariantRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StockRepository>().As<IStockRepository>().InstancePerLifetimeScope();
            #endregion
            #region DbContext
            builder.RegisterType<ProductContext>().As<IProductContext>().SingleInstance();
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
