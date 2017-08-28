using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Threax.AspNetCore.Tracking.Tests
{
    public class ModifiedTests
    {
        private void SetupMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                //Map the input model to the entity
                cfg.CreateMap<Object, ConcreteModified>()
                    .ForMember(d => d.Modified, opt => opt.ResolveUsing<IModifiedResolver>());
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
                var to = mapper.Map<ConcreteModified>(from);
                Assert.True(to.Modified != DateTime.MinValue);
            }
        }
    }
}
