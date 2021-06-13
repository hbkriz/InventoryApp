using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InventoryApp.Api.Data
{
    public abstract class BaseReader
    {
        private readonly IDbConnection _connection;
        private readonly ILogger<BaseReader> _logger;

        protected BaseReader(IDbConnection connection, ILogger<BaseReader> logger)
        {
            _connection = connection;
            _logger = logger;
        } 

        protected async Task<IEnumerable<T>> QueryStoredProcedureAsync<T>(string name, object parameters = null)
        {
            try
            {
                using (var connection = _connection)
                {
                    return await _connection.QueryAsync<T>(name, parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error due to {ex}");
                throw;
            }
        }
    }
}
