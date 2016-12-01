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
        [HttpGet("{schema}")]
        public String Get(String schema)
        {
            //Restrict to only the View Model namespace.
            var type = Type.GetType(schema);
            if(type == null)
            {
                throw new ErrorResultException($"Cannot find type {schema}.");
            }

            //Also make sure we have a HalModelAttribute on the class. 
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.GetCustomAttribute<HalModelAttribute>() == null)
            {
                throw new ErrorResultException($"Cannot find type {schema}.");
            }

            //Finally return the schema
            return JsonSchema4.FromType(type).ToJson();
        }
    }
}
