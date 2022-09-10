﻿using Core.Entities.ElasticSearch.Concrete;
using Core.Entities.ElasticSearch.DTOs;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.ElasticSearch
{
    public interface IElasticSearchService
    {
        Task<List<LogDetail>> Search(string keyword);
        Task<LogDetail> Add(LogDetail logDetail);


    }
}