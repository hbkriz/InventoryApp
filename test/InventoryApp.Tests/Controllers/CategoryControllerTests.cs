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
    public class CategoryControllerTests 
    {
        public Mock<ILogger<CategoryController>> _loggerMock;
        public Mock<IModelReader> _modelReaderMock;
        public CategoryController _controller;
        
        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<CategoryController>>(); 
            _modelReaderMock = new Mock<IModelReader>();
            _controller = new CategoryController(_loggerMock.Object, _modelReaderMock.Object);
        }

        [Test]
        public void GetAllAsyncTest()
        {

            //Arrange
            _modelReaderMock.Setup(x => x.GetCategories())
            .ReturnsAsync(new[]
            {
                new Category
                {
                    Id = 1,
                    Name = "C1"
                }
            });

            //Act & Assert
            Assert.DoesNotThrow(() => _controller.GetAllAsync());

            _modelReaderMock.Verify(x => x.GetCategories(), Times.Once);
                
        }
    }
}