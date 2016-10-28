using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Swashbuckle.Convention
{
    /// <summary>
    /// This filter adds jwt bearer headers to any operations that have the AuthorizeAttribute on them.
    /// Will also handle AllowAnonymousAttribute on functions inside a controller.
    /// </summary>
    public class AddJwtBearerHeaderParameter : IOperationFilter
    {
        private String headerName;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="headerName">The header name to user, defaults to bearer.</param>
        public AddJwtBearerHeaderParameter(String headerName = "bearer")
        {
            this.headerName = headerName;
        }

        /// <summary>
        /// Apply the header parameter.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var cad = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if(cad != null)
            {
                var controllerAttrs = cad.ControllerTypeInfo.CustomAttributes;
                var methodAttrs = cad.MethodInfo.CustomAttributes;
                var addHeader =
                    (controllerAttrs.Any(i => i.AttributeType == typeof(AuthorizeAttribute)) && !methodAttrs.Any(i => i.AttributeType == typeof(AllowAnonymousAttribute)))
                    || methodAttrs.Any(i => i.AttributeType == typeof(AuthorizeAttribute));

                if (addHeader)
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
    }
}
