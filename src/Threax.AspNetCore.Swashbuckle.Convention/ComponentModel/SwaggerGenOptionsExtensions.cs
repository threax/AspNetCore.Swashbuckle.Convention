using Swashbuckle.SwaggerGen.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Swashbuckle.Convention;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for the SwaggerGenOptions class.
    /// </summary>
    public static class SwaggerGenOptionsExtensions
    {
        /// <summary>
        /// Use the component model schema filter.
        /// </summary>
        /// <param name="options">The extended SwaggerGenOptions.</param>
        public static void UseComponentModel(this SwaggerGenOptions options)
        {
            options.SchemaFilter<ComponentModelSchemaFilter>();
        }
    }
}
