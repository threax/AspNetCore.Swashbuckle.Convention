using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Models;
using TestHalcyonApi.ViewModels;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.Database
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection UseAppDatabase(this IServiceCollection services)
        {
            //Add the database
            services.AddScoped<ThingyContext, ThingyContext>();

            //Setup the mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Thingy, ThingyView>();
                cfg.CreateMap<SubThingy, SubThingyView>();
                cfg.CreateMap<IEnumerable<Thingy>, ThingyCollectionView>()
                   .ForMember(dest => dest.Items,
                              opts => opts.MapFrom(src => src));
            });
            services.AddScoped<IMapper>(i => mapperConfig.CreateMapper());
            services.AddScoped<IHalModelViewMapper>(s =>
            {
                HalModelViewMapper viewMapper = new HalModelViewMapper();
                var mapper = s.GetRequiredService<IMapper>();
                foreach (var map in mapper.ConfigurationProvider.GetAllTypeMaps())
                {
                    viewMapper.AddConverter(map.SourceType, i => mapper.Map(i, map.SourceType, map.DestinationType));
                }
                return viewMapper;
            });

            return services;
        }
    }
}
