using Halcyon.HAL.Attributes;
using HateoasTest.Controllers;
using Newtonsoft.Json;
using NJsonSchema.Generation.TypeMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Controllers;
using TestHalcyonApi.Models;
using Threax.AspNetCore.Halcyon.Ext;
using NJsonSchema;
using NJsonSchema.Generation;
using NJsonSchema.Annotations;
using Threax.AspNetCore.Halcyon.Ext.ValueProviders;
using TestHalcyonApi.ValueProviders;
using Threax.AspNetCore.Halcyon.Ext.UIAttrs;
using TestHalcyonApi.Customizers;
using System.ComponentModel.DataAnnotations;

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
        [ValueProviderAttribute(typeof(OtherWeirdThingProvider))]
        [SelectUiType]
        [CustomizeSchema(typeof(PointlessSchemaCustomizer))]
        [Required]
        public Guid OtherWeirdThing { get; set; }

        [ValueProviderAttribute(typeof(OtherWeirdThingProvider))]
        [SelectUiType]
        [CustomizeSchema(typeof(PointlessSchemaCustomizer))]
        public Guid OtherWeirdThingNullable { get; set; }
    }
}
