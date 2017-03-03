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

        TypescriptClientWriter clientWriter;

        public ClientGenController(TypescriptClientWriter clientWriter)
        {
            this.clientWriter = clientWriter;
        }

        [HttpGet]
        [HalRel(Rels.Get)]
        public String Index()
        {
            return this.clientWriter.CreateClient();
        }
    }
}
