namespace EC.Services.ProductAPI.Settings.Abstract
{
    public interface IProductDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ProductsCollection { get; set; }
        string ProductVariantsCollection { get; set; }
        string VariantsCollection { get; set; }
        string StocksCollection { get; set; }
    }
}
