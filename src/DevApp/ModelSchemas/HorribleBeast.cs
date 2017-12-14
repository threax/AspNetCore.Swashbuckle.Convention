using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Threax.AspNetCore.Models;

namespace DevApp.ModelSchemas
{
    [PluralName("HorribleBeasties")]
    public abstract class HorribleBeast
    {
        [Required]
        [MaxLength(450)]
        [UiOrder]
        public String Name { get; set; }

        [UiOrder]
        public int NumEyes { get; set; }

        [UiOrder]
        public virtual int NumLegs { get; set; }
    }
}