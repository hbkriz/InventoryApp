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
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IModelReader _modelReader;

        public CategoryController(ILogger<CategoryController> logger, IModelReader modelReader)
        {
            _logger = logger;
            _modelReader = modelReader;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                return await _modelReader.GetCategories();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all categories");
                throw;
            }
        }
    }
}
