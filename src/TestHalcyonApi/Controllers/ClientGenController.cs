using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
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
        public ClientGenController()
        {
            
        }

        [HttpGet]
        public IActionResult Index([FromServices] IClientGenerator clientGenerator)
        {
            return new JsonResult(clientGenerator.GetEndpointDefinitions());
        }

        [HttpGet("[action]")]
        public IActionResult Typescript([FromServices] TypescriptClientWriter clientWriter)
        {
            using(var writer = new StringWriter())
            {
                clientWriter.CreateClient(writer);
                return Content(writer.ToString());
            }
        }
    }
}
