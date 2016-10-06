using Swashbuckle.SwaggerGen.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.Swagger.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Threax.AspNetCore.Swashbuckle.Convention
{
    public class ComponentModelSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var objectDisplayName = context.SystemType.GetTypeInfo().GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
            if(objectDisplayName != null)
            {
                model.Title = objectDisplayName.DisplayName;
            }

            if (model.Properties != null)
            {
                foreach (var propInfo in context.SystemType.GetProperties())
                {
                    var propertyName = "";
                    if (propInfo.Name.Length >= 1)
                    {
                        propertyName = propInfo.Name.Substring(0, 1).ToLowerInvariant();
                    }
                    if (propInfo.Name.Length > 1)
                    {
                        propertyName += propInfo.Name.Substring(1);
                    }

                    Schema property;
                    if (model.Properties.TryGetValue(propertyName, out property))
                    {
                        foreach (var attr in propInfo.GetCustomAttributes(true))
                        {
                            var displayName = attr as DisplayNameAttribute;
                            if (displayName != null)
                            {
                                property.Title = displayName.DisplayName;
                                continue;
                            }
                            var required = attr as RequiredAttribute;
                            if(required != null)
                            {
                                if (!model.Required.Contains(propertyName))
                                {
                                    model.Required.Add(propertyName);
                                }
                            }
                            var range = attr as RangeAttribute;
                            if(range != null)
                            {
                                property.Maximum = range.Maximum as int?;
                                property.Minimum = range.Minimum as int?;
                                property.ExclusiveMaximum = false;
                                property.ExclusiveMinimum = false;
                            }
                            var readOnly = attr as ReadOnlyAttribute;
                            property.ReadOnly = readOnly != null;
                        }
                    }
                }
            }
        }
    }
}
