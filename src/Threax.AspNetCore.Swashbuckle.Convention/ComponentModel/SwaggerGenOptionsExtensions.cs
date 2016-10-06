using Swashbuckle.SwaggerGen.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Swashbuckle.Convention;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerGenOptionsExtensions
    {
        public static void UseComponentModel(this SwaggerGenOptions options)
        {
            options.SchemaFilter<ComponentModelSchemaFilter>();
        }
    }
}
