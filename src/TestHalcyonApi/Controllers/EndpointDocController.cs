using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.ViewModels;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.Controllers
{
    [Route("api/[controller]")]
    public class EndpointDocController : Controller
    {
        IEndpointDocFinder endpointDocFinder;

        public EndpointDocController(IEndpointDocFinder endpointDocFinder)
        {
            this.endpointDocFinder = endpointDocFinder;
        }

        [HttpGet("{groupName}/{method}/{*relativePath}")]
        [HalRel(HalDocEndpointInfo.DefaultRels.Get)]
        public EndpointDescription Get(String groupName, String method, String relativePath)
        {
            return endpointDocFinder.FindDoc(groupName, method, relativePath);
        }
    }
}
