﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using TestHalcyonApi.Database;

namespace TestHalcyonApi
{
    public class Startup
    {
        private bool isDev;
        private Info apiInfo = new Info()
        {
            Version = "v1",
            Title = "Halcyon Test Api",
            Description = "A swagger view of our HATEOAS implemented using HAL via Halcyon.",
            TermsOfService = "None"
        };

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            isDev = env.IsEnvironment("Development");

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConventionalHalcyon();
            services.UseAppDatabase();

            services.AddMvc(o =>
            {
                o.UseExceptionErrorFilters(isDev);
                o.UseConventionalHalcyon();
            })
            .AddJsonOptions(o =>
            {
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddConventionalSwagger(apiInfo);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseConventionalSwagger(apiInfo);

            app.UseMvc();
        }
    }
}
