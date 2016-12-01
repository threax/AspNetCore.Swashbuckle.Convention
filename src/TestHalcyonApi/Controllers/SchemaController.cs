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
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.Controllers
{
    [Route("api/[controller]")]
    public class SchemaController : Controller
    {
        [HttpGet("{schema}")]
        public String Get([FromServices]ISchemaFinder schemaFinder, String schema)
        {
            return schemaFinder.Find(schema).ToJson();
        }
    }
}
