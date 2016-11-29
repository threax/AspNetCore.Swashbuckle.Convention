//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Halcyon.Web.HAL;
//using Halcyon.HAL;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.AspNetCore.Mvc.Routing;
//using Halcyon.HAL.Attributes;
//using AutoMapper;
//using TestHalcyonApi.Database;
//using TestHalcyonApi.ViewModels;

//namespace HateoasTest.Controllers
//{
//    [Route("api/[controller]")]
//    public class TestSubDataController : Controller
//    {
//        private IMapper mapper;
//        private ThingyContext testContext;

//        public TestSubDataController(ThingyContext testContext, IMapper mapper)
//        {
//            this.testContext = testContext;
//            this.mapper = mapper;
//        }

//        [HttpGet]
//        public SubThingyCollectionView List()
//        {
//            return new TestSubDataCollection(testContext.TestSubData.Select(i => mapper.Map<TestSubDataView>(i)));
//        }

//        [HttpGet("{TestSubDataId}")]
//        public TestSubDataView Get(int testSubDataId)
//        {
//            return mapper.Map<TestSubDataView>(testContext.TestSubData.First(i => i.TestSubDataId == testSubDataId));
//        }

//        // POST api/values
//        [HttpPost]
//        public void Add([FromBody]TestSubData value)
//        {
//        }

//        // PUT api/values/5
//        [HttpPut("{TestSubDataId}")]
//        public void Update(int testSubDataId, [FromBody]TestSubData value)
//        {
//        }

//        // DELETE api/values/5
//        [HttpDelete("{TestSubDataId}")]
//        public void Delete(int testSubDataId)
//        {
//        }
//    }
//}
