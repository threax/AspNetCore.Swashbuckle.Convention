using Halcyon.HAL;
using Halcyon.Web.HAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Database;
using TestHalcyonApi.Models;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.Controllers
{
    /// <summary>
    /// This controller shows how Halcyon allows us to create hal results directly. This shows
    /// a technique that will still pick up whatever convention you have set up for your model
    /// objects.
    /// </summary>
    [Route("api/[controller]")]
    public class RawHALThingyController : Controller
    {
        private ThingyContext testContext;
        private IHalModelViewMapper modelViewMapper;
        private IHALConverter halConverter;

        public RawHALThingyController(ThingyContext testContext, IHalModelViewMapper modelViewMapper, IHALConverter halConverter)
        {
            this.testContext = testContext;
            this.modelViewMapper = modelViewMapper;
            this.halConverter = halConverter;
        }

        [HttpGet]
        public IActionResult List()
        {
            //Using these interfaces makes it easy to use the conventions to discover properties and mappings.
            //If you wanted total control over the result, don't do this, but you are totally on your own
            //to define everything including links if you go this way.
            var converted = modelViewMapper.Convert(testContext.TestData);
            var hal = halConverter.Convert(converted).ToActionResult(this);
            //hal can be customized here if needed
            return hal;
        }

        [HttpGet("{ThingyId}")]
        public IActionResult Get(int thingyId)
        {
            var converted = modelViewMapper.Convert(testContext.TestData.First(i => i.ThingyId == thingyId));
            return halConverter.Convert(converted).ToActionResult(this);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]Thingy value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This should not list in any links since it has an authorize attribute unless you are logged in.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("[action]")]
        [Authorize]
        public IActionResult AuthorizedProperties([FromBody]Thingy value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This should not list in any links since it has a role the user will not have.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("[action]")]
        [Authorize(Roles = "NeverHaveThisRole")]
        public IActionResult RoleProperties([FromBody]Thingy value)
        {
            throw new NotImplementedException();
        }

        // PUT api/values/5
        [HttpPut("{ThingyId}")]
        public IActionResult Update(int testDataId, [FromBody]Thingy value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete("{ThingyId}")]
        public IActionResult Delete(int testDataId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{ThingyId}/SubThingy")]
        public IActionResult ListTestSubData(int thingyId)
        {
            var converted = modelViewMapper.Convert(testContext.TestSubData.Where(i => i.ThingyId == thingyId));
            return halConverter.Convert(converted).ToActionResult(this);
        }
    }
}
