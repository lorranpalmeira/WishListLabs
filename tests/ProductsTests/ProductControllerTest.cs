using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WishListLabs;
using WishListLabs.Config;
using WishListLabs.Models;
using WishListLabs.Repository;
using Xunit;

namespace tests.ProductsTests
{
    public class ProductControllerTest
    {

        Mock<IMongoCollection<Product>> mongoCollectionMock { get; set; }

        Mock<IContextMongoDb> contextMongoDbMock;

        Mock<IBaseRepository<Product>> baseRepoMock;

        public ProductControllerTest()
        {
            baseRepoMock = new Mock<IBaseRepository<Product>>();
            contextMongoDbMock = new Mock<IContextMongoDb>();
        }

        [Fact]
        public void GetAllTest()
        {
            var productList = new List<Product>
            {
                new Product{Id=01,Name="Produto 1"},
                new Product{Id=02,Name="Produto 2"},
                new Product{Id=03,Name="Produto 3"},
            };

            baseRepoMock.Setup(x => x.GetAll(3, 3))
                .ReturnsAsync(productList);

            ProductsController productsController = new ProductsController(baseRepoMock.Object, contextMongoDbMock.Object);

            var result = productsController.Get(3, 3);

            baseRepoMock.Verify(x => x.GetAll(3, 3), Times.Once);

            Assert.True(result.IsCompletedSuccessfully);
        }

        [Fact]
        public void PostTest()
        {
            var product = new Product { Id = 01, Name = "Produto 1" };
            baseRepoMock
                .Setup(x =>
                x.InsertItem(
                    It.IsAny<Product>()));

            var productsController = new ProductsController(baseRepoMock.Object, contextMongoDbMock.Object);
            var result = productsController.Post(product);

            baseRepoMock.Verify(x => x.InsertItem(
                It.IsAny<Product>()), 
                    Times.Once);

            Assert.True(result.IsCompletedSuccessfully);
        }

        [Fact]
        public void DeleteTest()
        {
            baseRepoMock.Setup(x => x.Delete(It.IsAny<int>()));

            ProductsController productController = new ProductsController(baseRepoMock.Object, contextMongoDbMock.Object);

            var result = productController.Delete(5);

            baseRepoMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);

            Assert.True(result.IsCompletedSuccessfully);
        }
    }
}
