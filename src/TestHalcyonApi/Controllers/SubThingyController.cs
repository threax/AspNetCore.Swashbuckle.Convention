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
using TestHalcyonApi.ViewModels;
using Threax.AspNetCore.Halcyon.Ext;

namespace HateoasTest.Controllers
{
    [Route("api/[controller]")]
    public class SubThingyController : Controller
    {
        public static class Rels
        {
            public const String List = "listSubThingies";
            public const String Get = "getSubThingy";
            public const String Add = "addSubThingy";
            public const String Update = "updateSubThingy";
            public const String Delete = "deleteSubThingy";
        }

        private IMapper mapper;
        private ThingyContext testContext;

        public SubThingyController(ThingyContext testContext, IMapper mapper)
        {
            this.testContext = testContext;
            this.mapper = mapper;
        }

        [HttpGet]
        [HalRel(Rels.List)]
        public SubThingyCollectionView List()
        {
            return mapper.Map<SubThingyCollectionView>(testContext.SubThingies.Values);
        }

        [HttpGet("{SubThingyId}")]
        [HalRel(Rels.Get)]
        public SubThingyView Get(int subThingyId)
        {
            return mapper.Map<SubThingyView>(testContext.SubThingies.Values.First(i => i.SubThingyId == subThingyId));
        }

        // POST api/values
        [HttpPost]
        [HalRel(Rels.Add)]
        public SubThingyView Add([FromBody]SubThingyView value)
        {
            var entity = mapper.Map<SubThingy>(value);
            testContext.SubThingies.Add(entity);
            return mapper.Map<SubThingyView>(entity);
        }

        // PUT api/values/5
        [HttpPut("{SubThingyId}")]
        [HalRel(Rels.Update)]
        public SubThingyView Update(int subThingyId, [FromBody]SubThingyView value)
        {
            value.ThingyId = subThingyId; //Make sure id is correct.
            var entity = testContext.Thingies.Get(subThingyId);
            mapper.Map(value, entity);
            return mapper.Map<SubThingyView>(entity);
        }

        // DELETE api/values/5
        [HttpDelete("{SubThingyId}")]
        [HalRel(Rels.Delete)]
        public void Delete(int subThingyId)
        {
            testContext.SubThingies.Remove(subThingyId);
        }
    }
}
