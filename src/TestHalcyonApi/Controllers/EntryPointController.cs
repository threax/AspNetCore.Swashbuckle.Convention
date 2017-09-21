using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.ViewModels;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.Controllers
{
    [Route("")]
    [ResponseCache(NoStore = true)]
    public class EntryPointController : Controller
    {
        public class Rels
        {
            public const String Get = "Get";
        }

        [HttpGet]
        [HalRel(Rels.Get)]
        public EntryPoints Get()
        {
            return new EntryPoints();
        }
    }
}
