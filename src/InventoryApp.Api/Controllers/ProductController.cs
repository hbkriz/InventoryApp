using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InventoryApp.Api.Models;
using InventoryApp.Api.Data.Interfaces;

namespace InventoryApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IModelReader _modelReader;

        public ProductController(ILogger<ProductController> logger, IModelReader modelReader)
        {
            _logger = logger;
            _modelReader = modelReader;
        }

        [HttpGet]
        [Route("featured")]
        public async Task<IEnumerable<Product>> GetFeaturedAsync()
        {
            try
            {
                return await _modelReader.GetFeaturedProducts();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting featured products");
                throw;
            }
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        public async Task<IEnumerable<Product>> GetFilteredAsync(int categoryId)
        {
            try
            {
                return await _modelReader.GetProductsByCategoryId(categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting products by category id");
                throw;
            }
        }
    }
}
