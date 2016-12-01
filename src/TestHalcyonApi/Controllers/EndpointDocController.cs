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
        ISchemaFinder schemaFinder;
        IApiDescriptionGroupCollectionProvider descriptionProvider;

        public EndpointDocController(ISchemaFinder schemaFinder, IApiDescriptionGroupCollectionProvider descriptionProvider)
        {
            this.schemaFinder = schemaFinder;
            this.descriptionProvider = descriptionProvider;
        }

        [HttpGet("{groupName}/{method}/{*relativePath}")]
        public EndpointDescription Get(String groupName, String method, String relativePath)
        {
            if(relativePath.EndsWith("/") || relativePath.EndsWith("\\"))
            {
                relativePath = relativePath.Substring(0, relativePath.Length - 1);
            }

            var group = descriptionProvider.ApiDescriptionGroups.Items.First(i => i.GroupName == groupName);
            var action = group.Items.First(i => i.HttpMethod == method && i.RelativePath == relativePath);

            var description = new EndpointDescription();
            foreach(var param in action.ParameterDescriptions)
            {
                if(param.Source.IsFromRequest && param.Source.Id == "Body")
                {
                    description.RequestSchemaLink = new HalSchemaLinkAttribute("requestSchema", typeof(SchemaController), "Get", param.Type);
                    description.RequestSchema = schemaFinder.Find(param.Type);
                }
            }

            var controllerActionDesc = action.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDesc != null)
            {
                var methodInfo = controllerActionDesc.MethodInfo;
                if(methodInfo.ReturnType != typeof(void))
                {
                    description.ResponseSchemaLink = new HalSchemaLinkAttribute("responseSchema", typeof(SchemaController), "Get", methodInfo.ReturnType);
                    description.ResponseSchema = schemaFinder.Find(methodInfo.ReturnType);
                }
            }

            return description;
        }
    }
}
