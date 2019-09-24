using BusinessServices.Search;
using DataAccess;
using DataAccess.CacheProvider;
using DataAccess.Search;
using Models.DTOs;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessServiceTests.Search
{
    [TestFixture]
    public class SearchServiceTests
    {
        private SearchModel searchResult;
        private ICacheProvider subCacheProvider;
        private ISearchRepository subSearchRepository;
        private IMemoryCache memoryCache;

        [Test]
        public async Task Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            SearchCriteriaModel model = new SearchCriteriaModel
            {
                Query = "Sipho"
            };

            // Act
            var result = await service.Get(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.People);
        }

        [SetUp]
        public void SetUp()
        {
            memoryCache = new MemoryCache(new MemoryCacheOptions { ExpirationScanFrequency = TimeSpan.FromMilliseconds(8) });

            searchResult = new SearchModel
            {
                Id = Guid.NewGuid().ToString(),
                People = new List<PersonModel>
                {
                    new PersonModel { Id = Guid.NewGuid().ToString(), Firstname = "John", Lastname = "Basic" },
                    new PersonModel { Id = Guid.NewGuid().ToString(), Firstname = "Jane", Lastname = "Basic" },
                    new PersonModel { Id = Guid.NewGuid().ToString(), Firstname = "Joan", Lastname = "Basic" },
                }
            };

            this.subSearchRepository = Substitute.For<ISearchRepository>();
            this.subSearchRepository
                .Get(Arg.Any<string>())
                .Returns(Task.FromResult(new DataAccess.Search.Search
                {
                    Id = Guid.NewGuid().ToString(),
                    People = new List<DataAccess.Person.Person>
                {
                    new DataAccess.Person.Person { Id = Guid.NewGuid().ToString(), Firstname = "John", Lastname = "Basic" },
                    new DataAccess.Person.Person { Id = Guid.NewGuid().ToString(), Firstname = "Jane", Lastname = "Basic" },
                    new DataAccess.Person.Person { Id = Guid.NewGuid().ToString(), Firstname = "Joan", Lastname = "Basic" },
                }
                }));

            this.subCacheProvider = Substitute.For<ICacheProvider>();
            this.subCacheProvider
                .Save(Arg.Any<string>(), searchResult)
                .Returns(true);
            this.subCacheProvider
                .Fetch<SearchModel>(Arg.Any<string>())
                .Returns(searchResult);

        }

        private SearchService CreateService()
        {
            return new SearchService(
                this.subSearchRepository,
                this.subCacheProvider);
        }

        [Test]
        public async Task IntegrationTest_SearchPeople()
        {
            // Arrange
            var connection = new Models.DTOs.Configuration.Connection
            {
                BoltURL = "bolt://localhost:7687",
                Username = "neo4j",
                Password = "n4j"
            };
            subSearchRepository = new SearchRepository(new Repository(connection));
            subCacheProvider = new InMemoryCache(memoryCache);
            var service = new SearchService(subSearchRepository, subCacheProvider);
            SearchCriteriaModel model = new SearchCriteriaModel
            {
                Query = "Sipho",
                First = 10,
                Offset = 0
            };

            // Act
            var result = await service.Get(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.People);
        }
    }
}