using Autofac;
using EC.Services.CategoryAPI.Data.Abstract.Dapper;
using EC.Services.CategoryAPI.Data.Abstract.EntityFramework;
using EC.Services.CategoryAPI.Data.Concrete.Dapper;
using EC.Services.CategoryAPI.Data.Concrete.EntityFramework;

namespace EC.Services.CategoryAPI.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Services - AddScoped
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().InstancePerLifetimeScope();


            #endregion



            //AddTransient
            //builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().InstancePerDependency();
        }
    }
}
