using Halcyon.HAL.Attributes;
using HateoasTest.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Models;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.ViewModels
{
    /// <summary>
    /// A simple test model.
    /// </summary>
    [HalModel()]
    [HalActionLink("self", typeof(ThingyController), "Get")]
    [HalActionLink("get", typeof(ThingyController), "Get")]
    [HalActionLink("update", typeof(ThingyController), "Update")]
    [HalActionLink("delete", typeof(ThingyController), "Delete")]
    [HalActionLink("listsubdata", typeof(ThingyController), "ListTestSubData")]
    [HalActionLink("addsubdata", typeof(ThingyController), "AddTestSubData")]
    [HalActionLink("authorizedproperties", typeof(ThingyController), "AuthorizedProperties")]
    [HalActionLink("roleproperties", typeof(ThingyController), "RoleProperties")]
    public class ThingyView : Thingy
    {
        
    }
}
