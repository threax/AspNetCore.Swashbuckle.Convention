using AutoMapper;
using DevApp.Controllers.Api;
using DevApp.Database;
using DevApp.InputModels;
using DevApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Threax.AspNetCore.Tests;
using Xunit;

namespace DevApp.Tests
{
    public static class ValueTests
    {
        private static void SetupCommonMockups(Mockup mockup)
        {
            mockup.Add<IMapper>(m => AppDatabaseServiceExtensions.SetupMappings().CreateMapper(mockup.Create));

            mockup.Add<AppDbContext>(m => new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                                                                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                                                        .Options));
        }

        public static ValueInput CreateInput(String seed = "Don't Care")
        {
            return new ValueInput()
            {
                Name = seed
            };
        }

        public class Repository : IDisposable
        {
            private Mockup mockup = new Mockup();

            public Repository()
            {
                SetupCommonMockups(mockup);
            }

            public void Dispose()
            {
                mockup.Dispose();
            }

            [Fact]
            async Task Add()
            {
                var repo = new ValueRepository(mockup.Create<AppDbContext>(), mockup.Create<IMapper>());
                var result = await repo.Add(CreateInput());
                Assert.NotNull(result);
            }

            [Fact]
            async Task AddRange()
            {
                var repo = new ValueRepository(mockup.Create<AppDbContext>(), mockup.Create<IMapper>());
                await repo.AddRange(new ValueInput[] { CreateInput(), CreateInput(), CreateInput() });
            }

            [Fact]
            async Task Delete()
            {
                var dbContext = mockup.Create<AppDbContext>();
                var repo = new ValueRepository(dbContext, mockup.Create<IMapper>());
                await repo.AddRange(new ValueInput[] { CreateInput(), CreateInput(), CreateInput() });
                var result = await repo.Add(CreateInput());
                Assert.Equal<int>(4, dbContext.Values.Count());
                await repo.Delete(result.ValueId);
                Assert.Equal<int>(3, dbContext.Values.Count());
            }

            [Fact]
            async Task Get()
            {
                var dbContext = mockup.Create<AppDbContext>();
                var repo = new ValueRepository(dbContext, mockup.Create<IMapper>());
                await repo.AddRange(new ValueInput[] { CreateInput(), CreateInput(), CreateInput() });
                var result = await repo.Add(CreateInput());
                Assert.Equal<int>(4, dbContext.Values.Count());
                var getResult = await repo.Get(result.ValueId);
                Assert.NotNull(getResult);
            }

            [Fact]
            async Task HasValuesEmpty()
            {
                var dbContext = mockup.Create<AppDbContext>();
                var repo = new ValueRepository(dbContext, mockup.Create<IMapper>());
                Assert.False(await repo.HasValues());
            }

            [Fact]
            async Task HasValues()
            {
                var dbContext = mockup.Create<AppDbContext>();
                var repo = new ValueRepository(dbContext, mockup.Create<IMapper>());
                await repo.AddRange(new ValueInput[] { CreateInput(), CreateInput(), CreateInput() });
                Assert.True(await repo.HasValues());
            }

            [Fact]
            async Task List()
            {
                //This could be more complete
                var dbContext = mockup.Create<AppDbContext>();
                var repo = new ValueRepository(dbContext, mockup.Create<IMapper>());
                await repo.AddRange(new ValueInput[] { CreateInput(), CreateInput(), CreateInput() });
                var query = new ValueQuery();
                var result = await repo.List(query);
                Assert.Equal(query.Limit, result.Limit);
                Assert.Equal(query.Offset, result.Offset);
                Assert.Equal(3, result.Total);
                Assert.NotEmpty(result.Items);
            }

            [Fact]
            async Task Update()
            {
                var repo = new ValueRepository(mockup.Create<AppDbContext>(), mockup.Create<IMapper>());
                var result = await repo.Add(CreateInput());
                Assert.NotNull(result);
                var updateResult = await repo.Update(result.ValueId, CreateInput());
                Assert.NotNull(updateResult);
            }
        }

        public class Controller : IDisposable
        {
            private Mockup mockup = new Mockup();

            public Controller()
            {
                SetupCommonMockups(mockup);
                mockup.Add<IValueRepository>(m => new ValueRepository(m.Create<AppDbContext>(), m.Create<IMapper>()));
                mockup.Add<ControllerContext>(m => new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                        {
                            new Claim(Threax.AspNetCore.AuthCore.ClaimTypes.ObjectGuid, Guid.NewGuid().ToString()),
                        }))
                    }
                });
            }

            public void Dispose()
            {
                mockup.Dispose();
            }

            [Fact]
            async Task List()
            {
                var totalItems = 3;

                var controller = new ValuesController(mockup.Create<IValueRepository>())
                {
                    ControllerContext = mockup.Create<ControllerContext>()
                };

                for(var i = 0; i < totalItems; ++i)
                {
                    Assert.NotNull(await controller.Add(CreateInput()));
                }

                var query = new ValueQuery();
                var result = await controller.List(query);
                Assert.Equal(query.Limit, result.Limit);
                Assert.Equal(query.Offset, result.Offset);
                Assert.Equal(3, result.Total);
                Assert.NotEmpty(result.Items);
            }

            [Fact]
            async Task Get()
            {
                var totalItems = 3;

                var controller = new ValuesController(mockup.Create<IValueRepository>())
                {
                    ControllerContext = mockup.Create<ControllerContext>()
                };

                for (var i = 0; i < totalItems; ++i)
                {
                    Assert.NotNull(await controller.Add(CreateInput()));
                }

                //Manually add the item we will look back up
                var lookup = await controller.Add(CreateInput());
                var result = await controller.Get(lookup.ValueId);
                Assert.NotNull(result);
            }

            [Fact]
            async Task Add()
            {
                var controller = new ValuesController(mockup.Create<IValueRepository>())
                {
                    ControllerContext = mockup.Create<ControllerContext>()
                };

                var result = await controller.Add(CreateInput());
                Assert.NotNull(result);
            }

            [Fact]
            async Task Update()
            {
                var controller = new ValuesController(mockup.Create<IValueRepository>())
                {
                    ControllerContext = mockup.Create<ControllerContext>()
                };

                var result = await controller.Add(CreateInput());
                Assert.NotNull(result);

                var updateResult = await controller.Update(result.ValueId, CreateInput());
                Assert.NotNull(updateResult);
            }

            [Fact]
            async Task Delete()
            {
                var controller = new ValuesController(mockup.Create<IValueRepository>())
                {
                    ControllerContext = mockup.Create<ControllerContext>()
                };

                var result = await controller.Add(CreateInput());
                Assert.NotNull(result);

                var listResult = await controller.List(new ValueQuery());
                Assert.Equal(1, listResult.Total);

                await controller.Delete(result.ValueId);

                listResult = await controller.List(new ValueQuery());
                Assert.Equal(0, listResult.Total);
            }
        }
    }
}
