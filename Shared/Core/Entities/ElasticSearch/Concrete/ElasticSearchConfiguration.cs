using Core.Entities.ElasticSearch.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.ElasticSearch.Concrete
{
    public class ElasticSearchConfigration : IElasticSearchConfigration
    {
        public IConfiguration Configuration { get; }
        public ElasticSearchConfigration(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string ConnectionString { get { return Configuration.GetSection("ELKConfiguration:Uri").Value; } }
        public string AuthUserName { get { return Configuration.GetSection("ELKConfiguration:UserName").Value; } }
        public string AuthPassWord { get { return Configuration.GetSection("ELKConfiguration:Password").Value; } }
    }
}
