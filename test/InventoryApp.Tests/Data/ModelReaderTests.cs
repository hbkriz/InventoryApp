using NUnit.Framework;
using InventoryApp.Api.Data;
using System.Data;
using Moq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using InventoryApp.Api.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Moq.Dapper;
using System.Linq;

namespace InventoryApp.Tests.Data
{
    [TestFixture]
    public class ModelReaderTests
    {
        private Mock<IDbConnection> _dbConnectionMock;
        private Mock<ILogger<BaseReader>> _loggerMock;
        private ModelReader _reader;
        private Task<IEnumerable<Category>> _categoryTask;
        private Task<IEnumerable<Product>> _productTask;
        
        [SetUp]
        public void Setup()
        {
            _dbConnectionMock = new Mock<IDbConnection>();
            _loggerMock = new Mock<ILogger<BaseReader>>();
            _reader = new ModelReader(_dbConnectionMock.Object,_loggerMock.Object);
        }

        [Test]
        public void GetCategoriesTest()
        {
            //Arrange
            var categoryResult = new[]
            {
                new Category
                {
                    Id = 1,
                    Name = "C1"
                },
                new Category
                {
                    Id = 2,
                    Name = "C2"
                }
            };
            _dbConnectionMock.SetupDapperAsync(x => x.QueryAsync<Category>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(categoryResult);
                
            //Act & Assert
            Assert.DoesNotThrow(() => _categoryTask = _reader.GetCategories());

            Assert.That(_categoryTask.Result.Count() == 2);
        }

        [Test]
        public void GetFeaturedProductsTest()
        {
            //Arrange
            var productResult = new[]
            {
                new Product
                {
                    Id = 1,
                    Name = "P1"
                },
                new Product
                {
                    Id = 2,
                    Name = "P2"
                },
                new Product
                {
                    Id = 3,
                    Name = "P3"
                }
            };
            _dbConnectionMock.SetupDapperAsync(x => x.QueryAsync<Product>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(productResult);
                
            //Act & Assert
            Assert.DoesNotThrow(() => _productTask = _reader.GetFeaturedProducts());

            Assert.That(_productTask.Result.Count() == 3);
        }

        [Test]
        public void GetProductsByCategoryId()
        {
            //Arrange
            var productResult = new[]
            {
                new Product
                {
                    Id = 1,
                    Name = "P1",
                    CategoryId = 2
                },
                new Product
                {
                    Id = 2,
                    Name = "P2",
                    CategoryId = 2
                }
            };
            _dbConnectionMock.SetupDapperAsync(x => x.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<int>(), null, null, null))
                .ReturnsAsync(productResult);
                
            //Act & Assert
            Assert.DoesNotThrow(() => _productTask = _reader.GetProductsByCategoryId(2));

            Assert.That(_productTask.Result.Count() == 2);
        }
    }
}