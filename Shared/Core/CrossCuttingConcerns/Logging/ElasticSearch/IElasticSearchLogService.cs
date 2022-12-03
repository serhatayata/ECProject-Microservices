using Core.Entities.ElasticSearch.Concrete;
using Core.Entities.ElasticSearch.DTOs;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.ElasticSearch
{
    public interface IElasticSearchLogService
    {
        Task<List<LogDetail>> GetAllAsync();
        Task<List<LogDetail>> GetMatchingByMethodNameAsync(string methodName);
        Task<List<LogDetail>> GetAllByRiskAsync(byte risk);
        Task<List<LogDetail>> SearchAsync(string keyword);
        Task<List<LogDetail>> SearchPagingAsync(string keyword,int page, int pageSize);
        Task<LogDetail> AddAsync(LogDetail logDetail);


    }
}
