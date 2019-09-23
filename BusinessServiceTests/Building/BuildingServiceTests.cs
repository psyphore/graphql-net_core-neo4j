using BusinessServices.Building;
using BusinessServices.Building.Extensions;
using DataAccess;
using DataAccess.Building;
using DataAccess.CacheProvider;
using DataAccess.Person;
using Models.DTOs;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entity = DataAccess.Building.Building;
using model = Models.DTOs.BuildingModel;

namespace BusinessServiceTests.Building
{
    [TestFixture]
    public class BuildingServiceTests
    {
        private List<entity> buildings;
        private IBuildingRepository subBuildingRepository;
        private ICacheProvider subCacheProvider;

        [Test]
        public async Task Add_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            var building = buildings.First().ToModel();

            // Act
            var result = await service.Add(building);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            var building = buildings.First().ToModel();

            // Act
            var result = await service.Create(building);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string id = buildings.First().Id;

            // Act
            var result = await service.Delete(id);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string id = buildings.First().Id;

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
        }

        [SetUp]
        public void SetUp()
        {
            buildings = new List<entity>
            {
                new entity{ Id = Guid.NewGuid().ToString(), Name = "A"},
                new entity{ Id = Guid.NewGuid().ToString(), Name = "B"},
                new entity{ Id = Guid.NewGuid().ToString(), Name = "C"},
                new entity{ Id = Guid.NewGuid().ToString(), Name = "D"},
            };

            this.subBuildingRepository = Substitute.For<IBuildingRepository>();
            this.subBuildingRepository
                .All()
                .Returns(buildings);

            this.subBuildingRepository
                .Get(Arg.Any<string>())
                .Returns(buildings[0]);

            this.subBuildingRepository
                .Delete(Arg.Any<string>())
                .Returns(Guid.NewGuid().ToString());

            this.subBuildingRepository
                .Add(Arg.Any<entity>())
                .Returns(buildings[0]);

            this.subBuildingRepository
                .Update(Arg.Any<entity>())
                .Returns(buildings[0]);

            this.subBuildingRepository
                .Get(Arg.Any<string>())
                .Returns(buildings[0]);

            this.subCacheProvider = Substitute.For<ICacheProvider>();
            this.subCacheProvider
                .Fetch<entity>(Arg.Any<string>())
                .Returns(buildings[0]);

            this.subCacheProvider
                .Save(Arg.Any<string>(), Arg.Any<entity>())
                .Returns(true);

            this.subCacheProvider
                .Save(Arg.Any<string>(), Arg.Any<model>())
                .Returns(true);
        }

        [Test]
        public async Task Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            var building = buildings.First().ToModel();

            // Act
            var result = await service.Update(building);

            // Assert
            Assert.IsNotNull(result);
        }

        private BuildingService CreateService()
        {
            return new BuildingService(
                this.subBuildingRepository,
                this.subCacheProvider);
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

            subBuildingRepository = new BuildingRepository(new Repository(connection));
            var service = new BuildingService(subBuildingRepository, subCacheProvider);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsNotNull(result);

            Assert.IsNotEmpty(result);

            Assert.IsNotNull(result.ToList().First().People);
        }
    }
}