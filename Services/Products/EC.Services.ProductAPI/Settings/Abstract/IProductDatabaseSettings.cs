namespace EC.Services.ProductAPI.Settings.Abstract
{
    public interface IProductDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}
