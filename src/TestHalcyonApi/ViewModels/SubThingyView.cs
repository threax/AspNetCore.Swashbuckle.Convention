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
    [HalSelfActionLink(SubThingyController.Rels.Get, typeof(SubThingyController))]
    [HalActionLink(SubThingyController.Rels.List, typeof(SubThingyController))]
    [HalActionLink(SubThingyController.Rels.Update, typeof(SubThingyController))]
    [HalActionLink(SubThingyController.Rels.Delete, typeof(SubThingyController))]
    [HalActionLink(SubThingyController.Rels.Get, typeof(SubThingyController))]
    [HalActionLink(ThingyController.Rels.Get, typeof(ThingyController))]
    public class SubThingyView : SubThingy
    {
    }
}
