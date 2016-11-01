using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.ExceptionToJson;

namespace Threax.AspNetCore.Swashbuckle.Convention
{
    /// <summary>
    /// This filter adds jwt bearer headers to any operations that have the AuthorizeAttribute on them.
    /// Will also handle AllowAnonymousAttribute on functions inside a controller.
    /// </summary>
    public class AddResponsesForValidation : IOperationFilter
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AddResponsesForValidation()
        {

        }

        /// <summary>
        /// Apply the header parameter.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var cad = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (cad != null)
            {
                var methodAttrs = cad.MethodInfo.CustomAttributes;

                var addResponse = methodAttrs.Any(i => i.AttributeType == typeof(AutoValidateAttribute));

                if (operation.Responses == null)
                {
                    operation.Responses = new Dictionary<String, Response>();
                }

                if (addResponse)
                {
                    operation.Responses.Add("400", new Response()
                    {
                        Description = "Model Validation Error",
                        Schema = context.SchemaRegistry.GetOrRegister(typeof(ModelStateErrorResult)),
                    });
                }

                operation.Responses.Add("500", new Response()
                {
                    Description = "Internal Server Error",
                    Schema = context.SchemaRegistry.GetOrRegister(typeof(ExceptionResult)),
                });
            }
        }
    }
}
