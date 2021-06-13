using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using InventoryApp.Api.Models;

namespace InventoryApp.Api.Data.Interfaces
{
    public interface IModelReader
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Product>> GetFeaturedProducts();
        Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId);
    }
}