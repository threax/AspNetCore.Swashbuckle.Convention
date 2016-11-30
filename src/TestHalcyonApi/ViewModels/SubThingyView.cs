using Halcyon.HAL.Attributes;
using HateoasTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Models;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.ViewModels
{
    [HalModel]
    [HalActionLink("self", typeof(SubThingyController), "Get")]
    [HalActionLink("get", typeof(SubThingyController), "Get")]
    [HalActionLink("update", typeof(SubThingyController), "Update")]
    [HalActionLink("delete", typeof(SubThingyController), "Delete")]
    public class SubThingyView : SubThingy
    {
    }
}
