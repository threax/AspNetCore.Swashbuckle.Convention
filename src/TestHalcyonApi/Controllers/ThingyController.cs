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
using TestHalcyonApi.ViewModels;

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
        private IMapper mapper;

        public ThingyController(ThingyContext testContext, IMapper mapper)
        {
            this.testContext = testContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public ThingyCollectionView List()
        {
            return mapper.Map<ThingyCollectionView>(testContext.TestData);
        }

        [HttpGet("{ThingyId}")]
        public ThingyView Get(int thingyId)
        {
            return mapper.Map<ThingyView>(testContext.TestData.First(i => i.ThingyId == thingyId));
        }

        // POST api/values
        [HttpPost]
        public ThingyView Add([FromBody]ThingyView value)
        {
            return value;
        }

        /// <summary>
        /// This should not list in any links since it has an authorize attribute unless you are logged in.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("[action]")]
        [Authorize]
        public ThingyView AuthorizedProperties([FromBody]ThingyView value)
        {
            return value;
        }

        /// <summary>
        /// This should not list in any links since it has a role the user will not have.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("[action]")]
        [Authorize(Roles="NeverHaveThisRole")]
        public ThingyView RoleProperties([FromBody]ThingyView value)
        {
            return value;
        }

        // PUT api/values/5
        [HttpPut("{ThingyId}")]
        public ThingyView Update(int testDataId, [FromBody]ThingyView value)
        {
            return value;
        }

        // DELETE api/values/5
        [HttpDelete("{ThingyId}")]
        public void Delete(int testDataId)
        {
        }

        [HttpGet("{ThingyId}/SubThingy")]
        public SubThingyCollectionView ListTestSubData(int thingyId)
        {
            return mapper.Map<SubThingyCollectionView>(testContext.TestSubData.Where(i => i.ThingyId == thingyId));
        }
    }
}
