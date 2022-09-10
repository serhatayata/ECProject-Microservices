using EC.Services.ProductAPI.Settings.Abstract;

namespace EC.Services.ProductAPI.Settings.Concrete
{
    public class ProductDatabaseSettings:IProductDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
