using Halcyon.HAL.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext;
using Halcyon.HAL;
using Newtonsoft.Json;

namespace TestHalcyonApi.ViewModels
{
    [HalModel]
    public class EndpointDescription : IHalLinkProvider
    {
        [JsonIgnore]
        public HalSchemaLinkAttribute RequestSchemaLink { get; set; }

        [JsonIgnore]
        public HalSchemaLinkAttribute ResponseSchemaLink { get; set; }

        public EndpointDescription()
        {

        }

        public IEnumerable<HalLinkAttribute> CreateHalLinks(ILinkProviderContext context)
        {
            if (RequestSchemaLink != null)
            {
                yield return RequestSchemaLink;
            }
            if(ResponseSchemaLink != null)
            {
                yield return ResponseSchemaLink;
            }
        }
    }
}
