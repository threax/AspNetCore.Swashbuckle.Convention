using DevApp.Controllers.Api;
using DevApp.Database;
using DevApp.InputModels;
using DevApp.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DevApp.Tests
{
    public class ValueTests
    {
        [Fact]
        public async Task DefaultQuery()
        {
            await TestQuery(new ValueQuery());
        }

        [Fact]
        public async Task Limit50Query()
        {
            await TestQuery(new ValueQuery()
            {
                Limit = 50
            });
        }

        [Fact]
        public async Task LastPageQuery()
        {
            await TestQuery(new ValueQuery()
            {
                Limit = 50,
                Offset = 5
            }, 255, 5);
        }

        [Fact]
        public async Task TestAdd()
        {
            var mappings = AppDatabaseServiceExtensions.SetupMappings();
            var mapper = mappings.CreateMapper();

            using (var dbContext = CreateDbContext())
            {
                var valueRepo = new ValueRepository(dbContext, mapper);
                await valueRepo.Add(new ValueInput()
                {
                    Name = "NewValue"
                });

                Assert.Equal("NewValue", dbContext.Values.First().Name);
            }
        }

        private Task TestQuery(ValueQuery query)
        {
            return TestQuery(query, 255, query.Limit);
        }

        private Task TestQuery(ValueQuery query, int totalItems)
        {
            return TestQuery(query, totalItems, query.Limit);
        }

        private async Task TestQuery(ValueQuery query, int totalItems, int totalReturned)
        {
            var mappings = AppDatabaseServiceExtensions.SetupMappings();
            var mapper = mappings.CreateMapper();

            var dbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
            var table = new List<ValueEntity>();
            dbContext.Setup(c => c.Values).ReturnsDbSet(CreateEntityTestValues(totalItems), table);

            var valueRepo = new ValueRepository(dbContext.Object, mapper);
            var valueCollection = await valueRepo.List(query);
            Assert.Equal(totalItems, valueCollection.Total);
            Assert.Equal(totalReturned, valueCollection.Items.Count());
            Assert.Equal(query.Limit, valueCollection.Limit);
            Assert.Equal(query.Offset, valueCollection.Offset);
        }

        private IEnumerable<ValueEntity> CreateEntityTestValues(int total)
        {
            for (int i = 0; i < total; ++i)
            {
                yield return new ValueEntity()
                {
                    Name = "Value " + i
                };
            }
        }

        private IEnumerable<ValueInput> CreateTestValues(int total)
        {
            for(int i = 0; i < total; ++i)
            {
                yield return new ValueInput()
                {
                    Name = "Value " + i
                };
            }
        }

        private static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            return new AppDbContext(options);
        }
    }
}
