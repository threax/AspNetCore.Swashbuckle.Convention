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
using TestHalcyonApi.Customizers;
using System.ComponentModel.DataAnnotations;
using Threax.AspNetCore.Models;

namespace TestHalcyonApi.ViewModels
{
    [HalModel]
    [HalSelfActionLink(typeof(SubThingyController), nameof(SubThingyController.Get))]
    [HalActionLink(typeof(SubThingyController), nameof(SubThingyController.List))]
    [HalActionLink(typeof(SubThingyController), nameof(SubThingyController.Update))]
    [HalActionLink(typeof(SubThingyController), nameof(SubThingyController.Delete))]
    [HalActionLink(typeof(SubThingyController), nameof(SubThingyController.Get))]
    [HalActionLink(typeof(ThingyController), nameof(ThingyController.Get))]
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
