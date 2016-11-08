using Microsoft.AspNetCore.Http;
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
    public class AddFileParamTypes : IOperationFilter
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
                var controllerAttrs = cad.ControllerTypeInfo.CustomAttributes;
                var methodAttrs = cad.MethodInfo.CustomAttributes;

                var methodParams = cad.MethodInfo.GetParameters();
                var hasIFormFile = false;

                foreach(var param in methodParams)
                {
                    //Single param
                    if (param.ParameterType == typeof(IFormFile))
                    {
                        //Swashbuckle likes to pick up the IFormFile parameters for the args, remove them
                        //Can't use FromBody on IFormFile, it won't work.
                        foreach(var iformParam in IFormFileProperties())
                        {
                            var item = operation.Parameters.FirstOrDefault(i => i.Name == iformParam);
                            if(item != null)
                            {
                                operation.Parameters.Remove(item);
                            }
                        }
                        var swaggerParam = new BodyParameter()
                        {
                            In = "formData",
                            Name = param.Name,
                        };
                        swaggerParam.Extensions.Add("type", "file");
                        operation.Parameters.Add(swaggerParam);
                    }

                    //Not supporting generics (collections probably), have to take one file at a time.
                    //Would like to though, put it here, here is some of the code to id it.
                    //var genericParams = param.ParameterType.GenericTypeArguments;
                    //genericParams.Any(i => i == typeof(IFormFile)) //this is the condition
                }

                if (hasIFormFile)
                {
                    operation.Consumes.Add("multipart/form-data");
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
