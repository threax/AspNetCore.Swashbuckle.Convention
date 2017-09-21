using Halcyon.HAL.Attributes;
using HateoasTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Controllers;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.ViewModels
{
    [HalModel]
    [HalEntryPoint]
    [HalSelfActionLink(typeof(EntryPointController), nameof(EntryPointController.Get))]
    [HalActionLink(typeof(ThingyController), nameof(ThingyController.List))]
    [HalActionLink(typeof(ThingyController), nameof(ThingyController.Add))]
    [HalActionLink(typeof(MultipartInputController), nameof(MultipartInputController.GetPart1), MultipartInputController.Rels.BeginAddMultipart)] //Here we bind a rel always named BeginAddMultipart to the real first step, this way we can alter the first step without changing the ui code.
    [HalActionLink(typeof(ThingyController), nameof(ThingyController.Update), DocsOnly = true)] //Test out sending only docs for the thingy update, this kind of thing is useful if you have a crud table that does not allow adds
    [HalActionLink(typeof(ThingyController), nameof(ThingyController.TestTakeListInput))]
    [HalActionLink(typeof(ThingyController), nameof(ThingyController.FileInput))]
    [HalActionLink(typeof(ThingyController), nameof(ThingyController.FileInputMultiple))]
    [HalActionLink(typeof(ThingyController), nameof(ThingyController.FileInputQuery))]
    [HalActionLink(typeof(ThingyController), nameof(ThingyController.FileInputMultipleQuery))]
    [HalActionLink(typeof(ThingyController), nameof(ThingyController.ReturnActionResult))]
    public class EntryPoints
    {
    }
}
