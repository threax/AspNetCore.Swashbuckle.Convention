using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.ClientGen;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.Controllers
{
    [Route("[controller]")]
    [ResponseCache(NoStore = true)]
    public class ClientGenController : Controller
    {
        public class Rels
        {
            public const String Get = "GetClient";
        }

        public ClientGenController()
        {
            
        }

        [HttpGet]
        [HalRel(Rels.Get)]
        public JsonResult Index([FromServices] IClientGenerator clientGenerator)
        {
            return new JsonResult(clientGenerator.GetEndpointDefinitions());
        }

        [HttpGet("[action]")]
        [HalRel(Rels.Get)]
        public String Typescript([FromServices] TypescriptClientWriter clientWriter)
        {
            using(var writer = new StringWriter())
            {
                clientWriter.CreateClient(writer);
                return writer.ToString();
            }
        }
    }
}
