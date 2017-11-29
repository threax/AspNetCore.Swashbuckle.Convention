using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Halcyon.HAL.Attributes;
using Threax.AspNetCore.Halcyon.Ext;
using Threax.AspNetCore.Halcyon.Ext.UIAttrs;
using Threax.AspNetCore.Tracking;
using DevApp.Models;
using DevApp.Controllers.Api;
namespace DevApp.ViewModels 
{
    [UiTitle("Value")]
    [HalModel]
    [HalSelfActionLink(typeof(ValuesController), nameof(ValuesController.Get))]
    [HalActionLink(typeof(ValuesController), nameof(ValuesController.Update))]
    [HalActionLink(typeof(ValuesController), nameof(ValuesController.Delete))]
    public class Value : IValue, IValueId , ICreatedModified
    {
        public Guid ValueId { get; set; }

        public String Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}