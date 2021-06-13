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
using InventoryApp.Api.Controllers;
using InventoryApp.Api.Data.Interfaces;

namespace InventoryApp.Tests.Controllers
{
    [TestFixture]
    public class ProductControllerTests 
    {
        public Mock<ILogger<ProductController>> _loggerMock;
        public Mock<IModelReader> _modelReaderMock;
        public ProductController _controller;
        
        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<ProductController>>(); 
            _modelReaderMock = new Mock<IModelReader>();
            _controller = new ProductController(_loggerMock.Object, _modelReaderMock.Object);
        }

        [Test]
        public void GetFeaturedAsyncTest()
        {

            //Arrange
            _modelReaderMock.Setup(x => x.GetFeaturedProducts())
            .ReturnsAsync(new[]
            {
                new Product
                {
                    Id = 1,
                    Name = "P1"
                }
            });

            //Act & Assert
            Assert.DoesNotThrow(() => _controller.GetFeaturedAsync());

            _modelReaderMock.Verify(x => x.GetFeaturedProducts(), Times.Once);               
        }

        [Test]
        public void GetFilteredAsyncTest()
        {
            //Arrange
            _modelReaderMock.Setup(x => x.GetProductsByCategoryId(It.IsAny<int>()))
            .ReturnsAsync(new[]
            {
                new Product
                {
                    Id = 1,
                    Name = "P1"
                }
            });

            //Act & Assert
            Assert.DoesNotThrow(() => _controller.GetFilteredAsync(1));

            _modelReaderMock.Verify(x => x.GetProductsByCategoryId(It.IsAny<int>()), Times.Once);      
        }
    }
}