using Autofac;
using EC.Services.ProductAPI.Data.Abstract;
using EC.Services.ProductAPI.Data.Concrete;
using EC.Services.ProductAPI.Repositories.Abstract;
using EC.Services.ProductAPI.Repositories.Concrete;

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



            //AddTransient
            //builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().InstancePerDependency();
        }
    }
}
