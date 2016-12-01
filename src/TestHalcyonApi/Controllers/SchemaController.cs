using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Halcyon.HAL.Attributes;
using Threax.AspNetCore.ExceptionToJson;
using NJsonSchema;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace TestHalcyonApi.Controllers
{
    [Route("api/[controller]")]
    public class SchemaController : Controller
    {
        private const String Namespace = "TestHalcyonApi.ViewModels.{0}";

        [HttpGet("{schema}")]
        public String Get(String schema)
        {
            //Restrict to only the View Model namespace.
            var typeName = String.Format(Namespace, schema);
            var type = Type.GetType(typeName);
            if(type == null)
            {
                throw new ErrorResultException($"Cannot find type {typeName}");
            }

            //Also make sure we have a HalModelAttribute on the class. 
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.GetCustomAttribute<HalModelAttribute>() == null)
            {
                throw new ErrorResultException($"Cannot find type {typeName}");
            }

            //Finally return the schema
            return JsonSchema4.FromType(type).ToJson();
        }
    }
}
