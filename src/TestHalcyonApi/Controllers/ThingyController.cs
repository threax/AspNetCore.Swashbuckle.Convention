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
        public static class Rels
        {
            public const String List = "listThingies";
            public const String Get = "getThingy";
            public const String Add = "addThingy";
            public const String Update = "updateThingy";
            public const String Delete = "deleteThingy";
            public const String AuthorizedProperties = "authorizedpropertiesThingies";
            public const String RoleProperties = "rolepropertiesThingies";
            public const String ListTestSubData = "listThingySubThingies";
        }

        private ThingyContext testContext;
        private IMapper mapper;

        public ThingyController(ThingyContext testContext, IMapper mapper)
        {
            this.testContext = testContext;
            this.mapper = mapper;
        }

        [HttpGet]
        [HalRel(Rels.List)]
        public ThingyCollectionView List()
        {
            return mapper.Map<ThingyCollectionView>(testContext.TestData);
        }

        [HttpGet("{ThingyId}")]
        [HalRel(Rels.Get)]
        public ThingyView Get(int thingyId)
        {
            return mapper.Map<ThingyView>(testContext.TestData.First(i => i.ThingyId == thingyId));
        }

        // POST api/values
        [HttpPost]
        [HalRel(Rels.Add)]
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
        [HalRel(Rels.AuthorizedProperties)]
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
        [HalRel(Rels.RoleProperties)]
        public ThingyView RoleProperties([FromBody]ThingyView value)
        {
            return value;
        }

        // PUT api/values/5
        [HttpPut("{ThingyId}")]
        [HalRel(Rels.Update)]
        public ThingyView Update(int testDataId, [FromBody]ThingyView value)
        {
            return value;
        }

        // DELETE api/values/5
        [HttpDelete("{ThingyId}")]
        [HalRel(Rels.Delete)]
        public void Delete(int testDataId)
        {
        }

        [HttpGet("{ThingyId}/SubThingy")]
        [HalRel(Rels.ListTestSubData)]
        public SubThingyCollectionView ListTestSubData(int thingyId)
        {
            return mapper.Map<SubThingyCollectionView>(testContext.TestSubData.Where(i => i.ThingyId == thingyId));
        }
    }
}
