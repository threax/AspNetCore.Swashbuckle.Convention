using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.ViewModels;

namespace TestHalcyonApi.Controllers
{
    [Route("api")]
    [ResponseCache(NoStore = true)]
    public class EntryPointController : Controller
    {
        [HttpGet]
        public EntryPoints List()
        {
            return new EntryPoints();
        }
    }
}
