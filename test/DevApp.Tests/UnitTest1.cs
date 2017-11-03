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

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            using (var dbContext = new AppDbContext(options))
            {
                //await dbContext.Database.EnsureCreatedAsync();
                await dbContext.Database.MigrateAsync();
                var valueRepo = new ValueRepository(dbContext, mapper);
                await valueRepo.AddRange(CreateTestValues(totalItems));
            }

            using (var dbContext = new AppDbContext(options))
            {
                var valueRepo = new ValueRepository(dbContext, mapper);
                var valueCollection = await valueRepo.List(query);
                Assert.Equal(totalItems, valueCollection.Total);
                Assert.Equal(totalReturned, valueCollection.Items.Count());
                Assert.Equal(query.Limit, valueCollection.Limit);
                Assert.Equal(query.Offset, valueCollection.Offset);
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
    }
}
