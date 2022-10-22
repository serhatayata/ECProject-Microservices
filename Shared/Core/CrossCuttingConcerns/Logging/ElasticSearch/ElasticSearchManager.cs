using Core.Entities.ElasticSearch.Abstract;
using Core.Entities.ElasticSearch.Concrete;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.ElasticSearch
{
    public class ElasticSearchManager:IElasticSearchService
    {
        public IElasticClient ElasticSearchClient { get; set; }
        private readonly IElasticSearchConfigration _elasticSearchConfigration;
        public ElasticSearchManager(IElasticSearchConfigration elasticSearchConfigration)
        {
            _elasticSearchConfigration = elasticSearchConfigration;
            ElasticSearchClient = GetClient();
        }

        private ElasticClient GetClient()
        {
            var str = _elasticSearchConfigration.ConnectionString;
            var strs = str.Split('|');
            var nodes = strs.Select(s => new Uri(s)).ToList();

            var connectionString = new ConnectionSettings(new Uri(str))
                //.DisablePing()
                //.SniffOnStartup(false)
                //.SniffOnConnectionFault(false)
                .DefaultIndex("ecproject");

            //if (!string.IsNullOrEmpty(_elasticSearchConfigration.AuthUserName) && !string.IsNullOrEmpty(_elasticSearchConfigration.AuthPassWord))
            //    connectionString.BasicAuthentication(_elasticSearchConfigration.AuthUserName, _elasticSearchConfigration.AuthPassWord);

            return new ElasticClient(connectionString);
        }
        #region SearchAsync
        public async Task<List<LogDetail>> SearchAsync(string keyword)
        {
            var results = await ElasticSearchClient.SearchAsync<LogDetail>(
                s => s.Query(
                    q => q.QueryString(
                        d => d.Query('*' + keyword + '*')
                    )
                ).Size(1000)
            );

            return results.Documents.ToList();
        }
        #endregion
        #region SearchPagingAsync
        public async Task<List<LogDetail>> SearchPagingAsync(string keyword,int page,int pageSize)
        {
            int from = (page - 1) * pageSize;
            var results = await ElasticSearchClient.SearchAsync<LogDetail>(
                s => s
                .From(from)
                .Size(pageSize)
                .Query(
                    q => q.QueryString(
                        d => d.Query('*' + keyword + '*')
                    )
                ).Size(1000)
            );

            return results.Documents.ToList();
        }
        #endregion
        #region Add
        public async Task<LogDetail> AddAsync(LogDetail logDetail)
        {
            var result = await ElasticSearchClient.IndexDocumentAsync(logDetail);
            return logDetail;
        }
        #endregion


    }
}
