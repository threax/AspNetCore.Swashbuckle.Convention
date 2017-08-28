using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Threax.AspNetCore.Tracking.Tests
{
    public class CreatedTests
    {
        private void SetupMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                //Map the input model to the entity
                cfg.CreateMap<Object, ConcreteCreated>()
                    .ForMember(d => d.Created, opt => opt.ResolveUsing<ICreatedResolver>());
            });

            services.AddScoped<IMapper>(s => mapperConfig.CreateMapper(s.GetRequiredService));
        }

        [Fact]
        public void Change()
        {
            var services = new ServiceCollection();
            SetupMapper(services);
            services.AddTimeTracking();

            var provider = services.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                var from = new Object();
                var to = mapper.Map<ConcreteCreated>(from);
                Assert.True(to.Created != DateTime.MinValue);
            }
        }

        [Fact]
        public void NoChange()
        {
            var services = new ServiceCollection();
            SetupMapper(services);
            services.AddTimeTracking();

            var provider = services.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                var date = new DateTime(2017, 01, 01);

                var from = new Object();
                var to = new ConcreteCreated()
                {
                    Created = date
                };

                mapper.Map(from, to);
                Assert.True(to.Created == date);
            }
        }
    }
}
