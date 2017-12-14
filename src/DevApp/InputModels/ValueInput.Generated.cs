using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Halcyon.HAL.Attributes;
using Threax.AspNetCore.Halcyon.Ext;
using DevApp.Models;
using Threax.AspNetCore.Halcyon.Ext.ValueProviders;
using Threax.AspNetCore.Models;

namespace DevApp.InputModels 
{
    [HalModel]
    public partial class ValueInput : IValue
    {
        [Required(ErrorMessage = "Name must have a value.")]
        [MaxLength(450, ErrorMessage = "Name must be less than 450 characters.")]
        [UiOrder(0, 24)]
        public String Name { get; set; }

    }
}
