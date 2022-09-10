using Core.DataAccess.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class RedisExtensions
    {
		public static void ConfigRedis(this IServiceCollection services, IConfiguration configuration)
		{
			var section = configuration.GetSection("Redis:Default");
			string _connectionString = section.GetSection("Connection").Value;
			string _instanceName = section.GetSection("InstanceName").Value;
			int _defaultDB = int.Parse(section.GetSection("DefaultDB").Value ?? "0");
			services.AddSingleton(new RedisClient(_connectionString, _instanceName, _defaultDB));
		}
	}
}
