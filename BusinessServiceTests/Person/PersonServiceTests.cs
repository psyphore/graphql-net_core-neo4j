using BusinessServices.Person;
using BusinessServices.Person.Extensions;
using DataAccess;
using DataAccess.CacheProvider;
using DataAccess.Person;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using entity = DataAccess.Person.Person;
using model = Models.DTOs.PersonModel;

namespace BusinessServiceTests.Person
{
    [TestFixture]
    public class PersonServiceTests
    {
        private ICacheProvider subCacheProvider;
        private IPersonRepository subPersonRepository;
        private List<entity> people;

        [Test]
        public async Task Add_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            var person = people[0].ToModel();

            // Act
            var result = await service.Add(person);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            model person = null;

            // Act
            var result = await service.Create(
                person);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string id = null;

            // Act
            var result = await service.Delete(
                id);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string id = people[0].Id;

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

        [SetUp]
        public void SetUp()
        {
            people = new List<entity>
            {
                new entity { Id = Guid.NewGuid().ToString(), Firstname = "John", Lastname = "Basic" },
                new entity { Id = Guid.NewGuid().ToString(), Firstname = "Jane", Lastname = "Basic" },
                new entity { Id = Guid.NewGuid().ToString(), Firstname = "Joan", Lastname = "Basic" },
            };

            this.subPersonRepository = Substitute.For<IPersonRepository>();
            this.subPersonRepository
                .All()
                .Returns(people);

            this.subPersonRepository
                .Get(Arg.Any<string>())
                .Returns(people[0]);

            this.subPersonRepository
                .Delete(Arg.Any<string>())
                .Returns(Guid.NewGuid().ToString());

            this.subPersonRepository
                .Add(Arg.Any<entity>())
                .Returns(people[0]);

            this.subPersonRepository
                .Update(Arg.Any<entity>())
                .Returns(people[0]);

            this.subPersonRepository
                .Get(Arg.Any<string>())
                .Returns(people[0]);

            this.subCacheProvider = Substitute.For<ICacheProvider>();
            this.subCacheProvider
                .Fetch<entity>(Arg.Any<string>())
                .Returns(people[0]);

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
            var person = people[0].ToModel();

            // Act
            var result = await service.Update(person);

            // Assert
            Assert.IsNotNull(result);
        }

        private PersonService CreateService()
        {
            return new PersonService(
                this.subPersonRepository,
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
            subPersonRepository = new PersonRepository(new Repository(connection));
            var service = new PersonService(subPersonRepository, subCacheProvider);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }
    }
}