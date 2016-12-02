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
        public SubThingyCollectionView List()
        {
            return mapper.Map<SubThingyCollectionView>(testContext.TestSubData);
        }

        [HttpGet("{SubThingyId}")]
        public SubThingyView Get(int subThingyId)
        {
            return mapper.Map<SubThingyView>(testContext.TestSubData.First(i => i.SubThingyId == subThingyId));
        }

        // POST api/values
        [HttpPost]
        public void Add([FromBody]SubThingyView value)
        {
        }

        // PUT api/values/5
        [HttpPut("{SubThingyId}")]
        public void Update(int subThingyId, [FromBody]SubThingyView value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{SubThingyId}")]
        public void Delete(int subThingyId)
        {
        }
    }
}
