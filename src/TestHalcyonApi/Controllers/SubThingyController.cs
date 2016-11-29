using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Halcyon.Web.HAL;
using Halcyon.HAL;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Halcyon.HAL.Attributes;
using AutoMapper;
using TestHalcyonApi.Database;
using TestHalcyonApi.Models;

namespace HateoasTest.Controllers
{
    [Route("api/[controller]")]
    public class SubThingyController : Controller
    {
        private IMapper mapper;
        private ThingyContext testContext;

        public SubThingyController(ThingyContext testContext, IMapper mapper)
        {
            this.testContext = testContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<SubThingy> List()
        {
            return testContext.TestSubData;
        }

        [HttpGet("{subThingyId}")]
        public SubThingy Get(int subThingyId)
        {
            return testContext.TestSubData.First(i => i.SubThingyId == subThingyId);
        }

        // POST api/values
        [HttpPost]
        public void Add([FromBody]SubThingy value)
        {
        }

        // PUT api/values/5
        [HttpPut("{subThingyId}")]
        public void Update(int subThingyId, [FromBody]SubThingy value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{subThingyId}")]
        public void Delete(int subThingyId)
        {
        }
    }
}
