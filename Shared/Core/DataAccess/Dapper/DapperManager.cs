using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Core.DataAccess.Dapper
{
    public class DapperManager : IDapperManager
    {
        private readonly IConfiguration _config;
        private readonly SqlConnection _sqlConnection;

        public DapperManager(IConfiguration config, string connectionString)
        {
            _config = config;
            _sqlConnection = new SqlConnection(connectionString);
        }
        public void Dispose()
        {

        }

        public DbConnection GetDbConnection()
        {
            return _sqlConnection;
        }

        #region GetAsync<T>
        public async Task<T> GetAsync<T>(string sql, DynamicParameters parameters,
    CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = GetDbConnection();
            return await db.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType)
                .ConfigureAwait(false);
        }
        #endregion
        #region GetAllAsync<T>
        public async Task<IEnumerable<T>> GetAllAsync<T>(string sql, DynamicParameters parameters,
    CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = GetDbConnection();
            return await db.QueryAsync<T>(sql, parameters, commandType: commandType).ConfigureAwait(false);
        }
        #endregion
        #region ExecuteAsync<T>
        public async Task<T> ExecuteAsync<T>(string sql, DynamicParameters parameters,
    CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = GetDbConnection();
            return await db.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType).ConfigureAwait(false);
        }
        #endregion
        #region InsertAsync<T>
        public async Task<T> InsertAsync<T>(string sql, DynamicParameters parameters,
    CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = await db.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType, transaction: tran).ConfigureAwait(false);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }
        #endregion
        #region UpdateAsync<T>
        public async Task<T> UpdateAsync<T>(string sql, DynamicParameters parameters,
    CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = await db.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType, transaction: tran).ConfigureAwait(false);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }
        #endregion

    }
}
