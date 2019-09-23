using BusinessServices.Product;
using DataAccess;
using DataAccess.CacheProvider;
using DataAccess.Product;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entity = DataAccess.Product.Product;
using model = Models.DTOs.ProductModel;

namespace BusinessServiceTests.Product
{
    [TestFixture]
    public class ProductServiceTests
    {
        private IProductRepository subProductRepository;
        private ICacheProvider subCacheProvider;
        private List<entity> products;

        [SetUp]
        public void SetUp()
        {
            products = new List<entity>
            {
                new entity { Id = Guid.NewGuid().ToString(), Name = "A" },
                new entity { Id = Guid.NewGuid().ToString(), Name = "B" },
                new entity { Id = Guid.NewGuid().ToString(), Name = "C" },
                new entity { Id = Guid.NewGuid().ToString(), Name = "D" },
                new entity { Id = Guid.NewGuid().ToString(), Name = "E" }
            };

            this.subProductRepository = Substitute.For<IProductRepository>();
            this.subProductRepository
                .Get(Arg.Any<string>())
                .Returns(products.First());

            this.subProductRepository
                .All()
                .Returns(products);

            this.subCacheProvider = Substitute.For<ICacheProvider>();
            this.subCacheProvider
                .Fetch<entity>(Arg.Any<string>())
                .Returns(products.First());

            this.subCacheProvider
                .Save(Arg.Any<string>(), Arg.Any<entity>())
                .Returns(true);

            this.subCacheProvider
                .Save(Arg.Any<string>(), Arg.Any<model>())
                .Returns(true);
        }

        private ProductService CreateService()
        {
            return new ProductService(
                this.subProductRepository,
                this.subCacheProvider);
        }

        [Test]
        public async Task Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string id = products.First().Id;

            // Act
            var result = await service.Get(id);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task IntegrationTest_GetPeople()
        {
            // Arrange
            var connection = new Models.DTOs.Configuration.Connection
            {
                BoltURL = "bolt://localhost:7687",
                Username = "neo4j",
                Password = "n4j"
            };
            subProductRepository = new ProductRepository(new Repository(connection));
            var service = new ProductService(subProductRepository, subCacheProvider);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);

            //Assert.IsNotNull(result.ToList().First().Owner);

            Assert.IsNotEmpty(result.ToList().First().Champions);
        }
    }
}
