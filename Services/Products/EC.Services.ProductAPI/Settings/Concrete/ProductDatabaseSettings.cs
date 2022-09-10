using EC.Services.ProductAPI.Settings.Abstract;

namespace EC.Services.ProductAPI.Settings.Concrete
{
    public class ProductDatabaseSettings:IProductDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ProductsCollection { get; set; }
        public string ProductVariantsCollection { get; set; }
        public string VariantsCollection { get; set; }
        public string StocksCollection { get; set; }
    }
}
