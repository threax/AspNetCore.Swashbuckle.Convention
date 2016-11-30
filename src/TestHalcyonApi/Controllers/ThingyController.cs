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
using Microsoft.AspNetCore.Authorization;
using Threax.AspNetCore.Halcyon.Ext;

namespace HateoasTest.Controllers
{
    /// <summary>
    /// This is a ThingyController that returns pocos. This should be the way most controllers
    /// can be built unless you need special logic when processing your links.
    /// </summary>
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

        /// <summary>
        /// This should not list in any links since it has an authorize attribute unless you are logged in.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("[action]")]
        [Authorize]
        public void AuthorizedProperties([FromBody]Thingy value)
        {
        }

        /// <summary>
        /// This should not list in any links since it has a role the user will not have.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("[action]")]
        [Authorize(Roles="NeverHaveThisRole")]
        public void RoleProperties([FromBody]Thingy value)
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
    }
}
