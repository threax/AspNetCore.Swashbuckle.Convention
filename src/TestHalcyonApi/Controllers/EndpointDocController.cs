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
        [HttpGet()]
        public String List([FromServices] IApiDescriptionGroupCollectionProvider descriptionProvider)
        {

            return "";
        }

        [HttpGet("{groupName}/{method}/{*relativePath}")]
        public EndpointDescription Get([FromServices] IApiDescriptionGroupCollectionProvider descriptionProvider, String groupName, String method, String relativePath)
        {
            var group = descriptionProvider.ApiDescriptionGroups.Items.First(i => i.GroupName == groupName);
            var action = group.Items.First(i => i.HttpMethod == method && i.RelativePath == relativePath);

            var description = new EndpointDescription();
            foreach(var param in action.ParameterDescriptions)
            {
                if(param.Source.IsFromRequest && param.Source.Id == "Body")
                {
                    description.RequestBodySchemaLink = new HalSchemaLinkAttribute("requestSchema", typeof(SchemaController), "Get", param.Type);
                }
            }

            var controllerActionDesc = action.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDesc != null)
            {
                var methodInfo = controllerActionDesc.MethodInfo;
                if(methodInfo.ReturnType != typeof(void))
                {
                    description.ResponseSchemaLink = new HalSchemaLinkAttribute("responseSchema", typeof(SchemaController), "Get", methodInfo.ReturnType);
                }
            }

            return description;
        }
    }
}
