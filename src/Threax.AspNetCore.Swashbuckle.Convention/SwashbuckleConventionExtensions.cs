using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.Swagger.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Threax.AspNetCore.Swashbuckle.Convention;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwashbuckleConventionExtensions
    {
        /// <summary>
        /// This sets up swagger in a common way that relies on convention to configure most of what is
        /// needed. This includes auto discovery of xml comments if they reside in the same folder as the
        /// program's main executable and are named the same as the executable, but with .xml as the extension.
        /// </summary>
        /// <remarks>
        /// You might need to enable xml comments in your project settings to get the comments working.
        /// </remarks>
        /// <param name="services">The service colletion.</param>
        /// <param name="apiInfo">The swagger info for your api.</param>
        /// <param name="options">Additional options, can be null for defaults.</param>
        /// <returns></returns>
        public static IServiceCollection AddConventionalSwagger(this IServiceCollection services, Info apiInfo, SwashbuckleConventionOptions options = null)
        {
            if(options == null)
            {
                options = new SwashbuckleConventionOptions();
            }

            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(o =>
            {
                o.SingleApiVersion(apiInfo);
                string pathToDoc = Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml");
                if (File.Exists(pathToDoc))
                {
                    o.IncludeXmlComments(pathToDoc);
                }
                o.DescribeAllEnumsAsStrings();
                o.UseComponentModel();
                if (options.HasJwtBearerAuth)
                {
                    o.OperationFilter<AddJwtBearerHeaderParameter>();
                }
                if (options.HasJsonExceptions)
                {
                    o.OperationFilter<AddResponsesForValidation>();
                }
            });

            return services;
        }

        /// <summary>
        /// This instructs the app to use your swagger convention. It will also auto discover the name of the app's
        /// virtual directory for swagger ui.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="apiInfo"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseConventionalSwagger(this IApplicationBuilder app, Info info)
        {
            return app.UseConventionalSwagger(new SwaggerConventionOptions()
            {
                ApiInfo = info
            });
        }

        /// <summary>
        /// This instructs the app to use your swagger convention. It will also auto discover the name of the app's
        /// virtual directory for swagger ui.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseConventionalSwagger(this IApplicationBuilder app, SwaggerConventionOptions options)
        {
            var baseUrlPath = "";
            var serverAddressFeature = app.ServerFeatures[typeof(IServerAddressesFeature)] as IServerAddressesFeature;
            if (serverAddressFeature != null)
            {
                var serverUrl = serverAddressFeature.Addresses.FirstOrDefault();
                if (serverUrl != null)
                {
                    var uri = new Uri(serverUrl);
                    baseUrlPath = uri.PathAndQuery;
                }
            }

            app.UseSwagger();
            if (options.UseSwaggerUi)
            {
                app.UseSwaggerUi($"{options.CustomBaseUrlPath}/ui", $"{baseUrlPath}{options.CustomBaseUrlPath}/{options.ApiInfo.Version}/swagger.json");
            }

            return app;
        }
    }
}
