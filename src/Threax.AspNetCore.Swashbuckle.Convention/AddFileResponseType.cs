using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Swashbuckle.Convention
{
    /// <summary>
    /// Add file upload parameters to an operation that has IFormFile parameters.
    /// </summary>
    public class AddFileResponseType : IOperationFilter
    {
        /// <summary>
        /// Apply function.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var cad = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (cad != null)
            {
                var returnType = cad.MethodInfo.ReturnType;

                //Single param
                if (returnType == typeof(FileStreamResult))
                {
                    //Add file schema to all 200 responses
                    foreach(var response in operation.Responses)
                    {
                        int value;
                        if(int.TryParse(response.Key, out value) && value > 199 && value < 300)
                        {
                            response.Value.Schema = new Schema()
                            {
                                Type = "file"
                            };
                        }
                    }
                }
            }
        }

        private IEnumerable<String> IFormFileProperties()
        {
            foreach(var property in typeof(IFormFile).GetProperties())
            {
                yield return property.Name;
            }
        }
    }
}
