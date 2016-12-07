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
            //Add the database, normally this would be scoped, but we are in memory
            services.AddSingleton<ThingyContext, ThingyContext>();

            //Setup the mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Thingy, ThingyView>();
                cfg.CreateMap<SubThingy, SubThingyView>();
                cfg.CreateMap<ThingyView, Thingy>();
                cfg.CreateMap<SubThingyView, SubThingy>();
                cfg.CreateMap<IEnumerable<SubThingy>, SubThingyCollectionView>()
                   .ForMember(dest => dest.Items,
                              opts => opts.MapFrom(src => src));

                //Also map models to themselves to handle fake database, wont need this normally
                cfg.CreateMap<Thingy, Thingy>();
                cfg.CreateMap<SubThingy, SubThingy>();
            });
            services.AddScoped<IMapper>(i => mapperConfig.CreateMapper());

            return services;
        }
    }
}
