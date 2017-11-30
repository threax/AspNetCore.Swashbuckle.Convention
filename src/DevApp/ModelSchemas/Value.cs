﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Threax.AspNetCore.Models;

namespace DevApp.ModelSchemas
{
    //[PluralName("ValueWoots")]
    [RequireAuthorization(typeof(Roles), nameof(Roles.EditValues))]
    //[RequireAuthorization(Roles.EditValues)] //Alt syntax, not ideal
    public abstract class Value
    {
        [Required]
        [MaxLength(450)]
        //[Display(Name = "WootName")]
        public String Name { get; set; }
    }
}