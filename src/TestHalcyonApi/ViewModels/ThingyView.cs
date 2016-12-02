using Halcyon.HAL.Attributes;
using HateoasTest.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Controllers;
using TestHalcyonApi.Models;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.ViewModels
{
    /// <summary>
    /// A simple test model.
    /// </summary>
    [HalModel]
    [HalSelfActionLink(ThingyController.Rels.Get, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.Get, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.Update, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.Delete, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.ListTestSubData, typeof(ThingyController))]
    [HalActionLink(SubThingyController.Rels.Add, typeof(SubThingyController))]
    [HalActionLink(ThingyController.Rels.AuthorizedProperties, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.RoleProperties, typeof(ThingyController))]
    public class ThingyView : Thingy
    {
        
    }
}
