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
    public class ThingyController : Controller
    {
        private ThingyContext testContext;

        public ThingyController(ThingyContext testContext)
        {
            this.testContext = testContext;
        }

        [HttpGet]
        public IEnumerable<Thingy> List()
        {
            return testContext.TestData;
        }

        [HttpGet("{ThingyId}")]
        public Thingy Get(int thingyId)
        {
            return testContext.TestData.First(i => i.ThingyId == thingyId);
        }

        // POST api/values
        [HttpPost]
        public void Add([FromBody]Thingy value)
        {
        }

        // PUT api/values/5
        [HttpPut("{ThingyId}")]
        public void Update(int testDataId, [FromBody]Thingy value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{ThingyId}")]
        public void Delete(int testDataId)
        {
        }

        [HttpGet("{ThingyId}/SubThingy")]
        public IEnumerable<SubThingy> ListTestSubData(int thingyId)
        {
            return testContext.TestSubData.Where(i => i.ThingyId == thingyId);
        }

        [HttpPost("{ThingyId}/SubThingy")]
        public void AddTestSubData(int testDataId)
        {

        }

        [HttpPut("{ThingyId}/SubThingy/{SubThingyId}")]
        public void UpdateTestSubData(int testDataId, int subThingyId)
        {

        }

        [HttpDelete("{ThingyId}/SubThingy/{SubThingyId}")]
        public void DeleteTestSubData(int testDataId, int subThingyId)
        {
            
        }
    }
}
