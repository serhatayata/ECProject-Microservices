using Core.Entities;
using Core.Entities.ElasticSearch.Abstract;
using Core.Entities.ElasticSearch.Concrete;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Core.CrossCuttingConcerns.Logging.ElasticSearch
{
    public class ElasticSearchLogManager:IElasticSearchLogService
    {
        public IElasticClient client { get; set; }
        private readonly IElasticSearchConfiguration _elasticSearchConfigration;
        public ElasticSearchLogManager(IElasticSearchConfiguration elasticSearchConfigration)
        {
            _elasticSearchConfigration = elasticSearchConfigration;
            client = GetClient();
        }

        private ElasticClient GetClient()
        {
            var str = _elasticSearchConfigration.ConnectionString;
            var strs = str.Split('|');
            var nodes = strs.Select(s => new Uri(s)).ToList();

            var connectionString = new ConnectionSettings(new Uri(str))
                .DefaultIndex("ecproject-log");

            //if (!string.IsNullOrEmpty(_elasticSearchConfigration.AuthUserName) && !string.IsNullOrEmpty(_elasticSearchConfigration.AuthPassWord))
            //    connectionString.BasicAuthentication(_elasticSearchConfigration.AuthUserName, _elasticSearchConfigration.AuthPassWord);

            return new ElasticClient(connectionString);
        }

        #region GetAllAsync
        /// <summary>
        /// Get all log details
        /// </summary>
        /// <returns></returns>
        public async Task<List<LogDetail>> GetAllAsync()
        {
            var result = await client.SearchAsync<LogDetail>(s =>
              s.Query(q => q.MatchAll())
            );

            return result.Documents.ToList();
        }
        #endregion
        #region GetAllByRiskAsync
        /// <summary>
        /// Get all log details by risk and it is the exact risk value
        /// </summary>
        /// <param name="risk"></param>
        /// <returns></returns>
        public async Task<List<LogDetail>> GetAllByRiskAsync(byte risk)
        {
            var result = await client.SearchAsync<LogDetail>(s =>
               s.Query(q => 
                  q.Term(t => 
                     t.Field(ld =>
                        ld.Risk
                     ).Value(risk)
                  )
               )
            );

            return result.Documents.ToList();
        }
        #endregion
        #region GetMatchingByMethodNameAsync
        /// <summary>
        /// Gets the list of matching log details, not exact method name
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public async Task<List<LogDetail>> GetMatchingByMethodNameAsync(string methodName)
        {
            var result = await client.SearchAsync<LogDetail>(s =>
               s.Query(q => 
                  q.Match(t =>
                     t.Field(f => f.MethodName)
                     .Query(methodName)
                  )
               )
            );

            return result.Documents.ToList();
        }
        #endregion
        #region SearchAsync
        /// <summary>
        /// Gets the logs details with matching keyword for all fields
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<LogDetail>> SearchAsync(string keyword)
        {
            var results = await client.SearchAsync<LogDetail>(
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
        /// <summary>
        /// Gets the logs details with matching keyword for all fields by paging
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<LogDetail>> SearchPagingAsync(string keyword,int page,int pageSize)
        {
            int from = (page - 1) * pageSize;
            var results = await client.SearchAsync<LogDetail>(
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
        /// <summary>
        /// Adds a log detail
        /// </summary>
        /// <param name="logDetail"></param>
        /// <returns></returns>
        public async Task<LogDetail> AddAsync(LogDetail logDetail)
        {
            var result = await client.IndexDocumentAsync(logDetail);
            return logDetail;
        }
        #endregion


    }
}
