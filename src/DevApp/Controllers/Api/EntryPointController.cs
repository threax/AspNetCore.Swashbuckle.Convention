using DevApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Threax.AspNetCore.Halcyon.Ext;

namespace DevApp.Controllers.Api
{
    [Route("api")]
    [ResponseCache(NoStore = true)]
    [Authorize(AuthenticationSchemes = AuthCoreSchemes.Bearer)]
    public class EntryPointController : Controller
    {
        public class Rels
        {
            public const String Get = "GetEntryPoint";
        }

        [HttpGet]
        [HalRel(Rels.Get)]
        [AllowAnonymous]
        public EntryPoint Get()
        {
            return new EntryPoint();
        }
    }
}
