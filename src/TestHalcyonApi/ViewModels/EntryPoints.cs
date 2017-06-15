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
    [HalSelfActionLink(EntryPointController.Rels.Get, typeof(EntryPointController))]
    [HalActionLink(ThingyController.Rels.List, typeof(ThingyController))]
    [HalActionLink(MultipartInputController.Rels.BeginAddMultipart, MultipartInputController.Rels.GetPart1, typeof(MultipartInputController))] //Here we bind a rel always named BeginAddMultipart to the real first step, this way we can alter the first step without changing the ui code.
    [HalActionLink(ThingyController.Rels.Update, typeof(ThingyController), DocsOnly = true)] //Test out sending only docs for the thingy update, this kind of thing is useful if you have a crud table that does not allow adds
    [HalActionLink(ThingyController.Rels.TestTakeListInput, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.FileInput, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.FileInputMultiple, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.FileInputQuery, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.FileInputMultipleQuery, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.ReturnActionResult, typeof(ThingyController))]
    public class EntryPoints
    {
    }
}
