using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Swashbuckle.Convention
{
    public class AddJwtBearerHeaderParameter : IOperationFilter
    {
        private String headerName;

        public AddJwtBearerHeaderParameter(String headerName = "bearer")
        {
            this.headerName = headerName;
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = headerName,
                In = "header",
                Type = "string",
                Required = true
            });
        }
    }
}
