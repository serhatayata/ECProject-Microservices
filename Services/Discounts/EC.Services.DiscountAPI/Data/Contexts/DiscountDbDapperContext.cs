using Microsoft.Data.SqlClient;
using System.Data;

namespace EC.Services.DiscountAPI.Data.Contexts
{
    public class DiscountDbDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DiscountDbDapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}