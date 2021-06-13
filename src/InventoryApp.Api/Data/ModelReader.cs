using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using InventoryApp.Api.Models;
using InventoryApp.Api.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace InventoryApp.Api.Data
{
    public class ModelReader : BaseReader, IModelReader
    {
        public ModelReader(IDbConnection connection, ILogger<BaseReader> logger) : base(connection, logger)
        {
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await QueryStoredProcedureAsync<Category>("spGetCategories");
        }

        public async Task<IEnumerable<Product>> GetFeaturedProducts()
        {
            return await QueryStoredProcedureAsync<Product>("spGetFeaturedProducts");
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            return await QueryStoredProcedureAsync<Product>("spGetProductsByCategoryId", new {CategoryId = categoryId});
        }
    }
}
