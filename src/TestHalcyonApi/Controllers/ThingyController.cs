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
    /// This is a ThingyController that returns view models. This is the general pattern
    /// of what you want to return. The view models define the relationships between the 
    /// controllers and the framework will automatically figure out if the user can actually
    /// visit the returned link.
    /// </summary>
    /// <remarks>
    /// Ignore the way data is put into the context, you will probably want to use entity framework.
    /// </remarks>
    [Route("[controller]")]
    [ResponseCache(NoStore = true)]
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
            public const String TestDeclareLinkToRel = "testDeclareLinkToRel";
        }

        private ThingyContext testContext;
        private IMapper mapper;

        public ThingyController(ThingyContext testContext, IMapper mapper)
        {
            this.testContext = testContext;
            this.mapper = mapper;
        }

        [HttpGet]
        [HalRel(Rels.List, IsPaged = true)]
        public ThingyCollectionView List([FromQuery] CollectionQuery query)
        {
            var items = testContext.Thingies.Values.Skip(query.Offset * query.Limit).Take(query.Limit).Select(i => mapper.Map<ThingyView>(i));
            return new ThingyCollectionView(query.Offset, query.Limit, testContext.Thingies.Values.Count(), items);
        }

        [HttpGet("{ThingyId}")]
        [HalRel(Rels.Get)]
        public ThingyView Get(int thingyId)
        {
            return mapper.Map<ThingyView>(testContext.Thingies.Values.First(i => i.ThingyId == thingyId));
        }

        // POST api/values
        [HttpPost]
        [HalRel(Rels.Add)]
        [AutoValidate]
        public ThingyView Add([FromBody]ThingyView value)
        {
            //This is the general idea for an add, but if you were using entity framework you would do the correct calls there
            //but the general pattern of map to entity -> update -> get updated object-> return updated object will apply
            var entity = mapper.Map<Thingy>(value);
            testContext.Thingies.Add(entity);
            return mapper.Map<ThingyView>(entity);
        }

        // PUT api/values/5
        [HttpPut("{ThingyId}")]
        [HalRel(Rels.Update)]
        [AutoValidate]
        public ThingyView Update(int thingyId, [FromBody]ThingyView value)
        {
            value.ThingyId = thingyId; //Make sure id is correct.
            var entity = testContext.Thingies.Get(thingyId);
            mapper.Map(value, entity);
            return mapper.Map<ThingyView>(entity);
        }

        // DELETE api/values/5
        [HttpDelete("{ThingyId}")]
        [HalRel(Rels.Delete)]
        public void Delete(int thingyId)
        {
            testContext.Thingies.Remove(thingyId);
        }

        [HttpGet()]
        [Route("{ThingyId}/SubThingy")]
        [HalRel(Rels.ListTestSubData)]
        public SubThingyCollectionView ListTestSubData(int thingyId)
        {
            var items = testContext.SubThingies.Values.Where(i => i.ThingyId == thingyId).Select(i => mapper.Map<SubThingyView>(i));
            return new SubThingyCollectionView(items);
        }

        //Some test stuff below here, mostly to test roles, you should never see these since you can't log into the browser

        /// <summary>
        /// This should not list in any links since it has an authorize attribute unless you are logged in.
        /// </summary>
        [HttpPost("[action]")]
        [Authorize]
        [HalRel(Rels.AuthorizedProperties)]
        public void AuthorizedProperties()
        {

        }

        /// <summary>
        /// This should not list in any links since it has a role the user will not have.
        /// </summary>
        [HttpPost("[action]")]
        [Authorize(Roles = "NeverHaveThisRole")]
        [HalRel(Rels.RoleProperties)]
        public void RoleProperties()
        {

        }

        /// <summary>
        /// This should not list in any links since it has a role the user will not have.
        /// </summary>
        [HttpGet("[action]")]
        [HalRel(Rels.TestDeclareLinkToRel)]
        public void TestDeclaredLinkToRel([FromQuery] CollectionQuery query)
        {

        }
    }
}
