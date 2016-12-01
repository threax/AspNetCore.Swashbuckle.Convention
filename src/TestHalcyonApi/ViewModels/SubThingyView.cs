using Halcyon.HAL.Attributes;
using HateoasTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Controllers;
using TestHalcyonApi.Models;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.ViewModels
{
    [HalModel]
    [HalActionLink("self", typeof(SubThingyController), "Get")]
    [HalActionLink("get", typeof(SubThingyController), "Get")]
    [HalActionLink("update", typeof(SubThingyController), "Update")]
    [HalActionLink("delete", typeof(SubThingyController), "Delete")]
    [HalActionLink("getthingy", typeof(ThingyController), "Get")]
    [HalSchemaLink("schema.self", typeof(SchemaController), "Get", typeof(SubThingyView))]
    public class SubThingyView : SubThingy
    {
    }
}
