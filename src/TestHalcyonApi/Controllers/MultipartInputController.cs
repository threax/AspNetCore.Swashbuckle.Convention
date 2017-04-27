using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Models;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.Controllers
{
    /// <summary>
    /// This is a simple example of multipart input. You can go along controller actions to
    /// submit each part of the data and tie them together using the hal links.
    /// </summary>
    [Route("[controller]/[action]")] //Be sure to take actions here since you will have multiple gets and sets
    [ResponseCache(NoStore = true)]
    public class MultipartInputController : Controller
    {
        public static class Rels
        {
            public const String GetPart1 = "GetPart1";
            public const String SetPart1 = "SetPart1";
            public const String GetPart2 = "GetPart2";
            public const String SetPart2 = "SetPart2";
            public const String GetPart3 = "GetPart3";
            public const String SetPart3 = "SetPart3";

            public const String BeginAddMultipart = "BeginAddMultipart"; //This isn't used directly, but is remapped to another phase, this way we can change the start point without changing the client
        }

        [HttpGet]
        [HalRel(Rels.GetPart1)]
        [Route("{ExamRequestId}")]
        public Task<MultipartInput1> GetPart1(Guid multipartInputModelId)
        {
            //Normally you would look at the guid. If it is Guid.Empty add a new model to the database, generating a id for it
            //then return that result. If the guid is not Guid.Empty attempt to lookup the data and return that. Throw an
            //error in that case if the guid cannot be found, don't create it again.
            return Task.FromResult(new MultipartInput1());
        }

        [HttpPut("{ExamRequestId}")]
        [HalRel(Rels.SetPart1)]
        [AutoValidate]
        public Task<MultipartInput1> SetPart1(Guid examRequestId, [FromBody]MultipartInput1 value)
        {
            //Update the full model from the partial input here, not shown since this is just a structural example
            return Task.FromResult(value); //No-op
        }

        [HttpGet]
        [HalRel(Rels.GetPart2)]
        [Route("{ExamRequestId}")]
        public Task<MultipartInput2> GetPart2(Guid multipartInputModelId)
        {
            //Normally you would look at the guid. If it is Guid.Empty add a new model to the database, generating a id for it
            //then return that result. If the guid is not Guid.Empty attempt to lookup the data and return that. Throw an
            //error in that case if the guid cannot be found, don't create it again.
            return Task.FromResult(new MultipartInput2());
        }

        [HttpPut("{ExamRequestId}")]
        [HalRel(Rels.SetPart2)]
        [AutoValidate]
        public Task<MultipartInput2> SetPart2(Guid examRequestId, [FromBody]MultipartInput2 value)
        {
            //Update the full model from the partial input here, not shown since this is just a structural example
            return Task.FromResult(value); //No-op
        }

        [HttpGet]
        [HalRel(Rels.GetPart3)]
        [Route("{ExamRequestId}")]
        public Task<MultipartInput3> GetPart3(Guid multipartInputModelId)
        {
            //Normally you would look at the guid. If it is Guid.Empty add a new model to the database, generating a id for it
            //then return that result. If the guid is not Guid.Empty attempt to lookup the data and return that. Throw an
            //error in that case if the guid cannot be found, don't create it again.
            return Task.FromResult(new MultipartInput3());
        }

        [HttpPut("{ExamRequestId}")]
        [HalRel(Rels.SetPart3)]
        [AutoValidate]
        public Task<MultipartInput3> SetPart3(Guid examRequestId, [FromBody]MultipartInput3 value)
        {
            //Update the full model from the partial input here, not shown since this is just a structural example
            return Task.FromResult(value); //No-op
        }
    }
}
