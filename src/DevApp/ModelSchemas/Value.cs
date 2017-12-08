using DevApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Threax.AspNetCore.Halcyon.Ext.UIAttrs;
using Threax.AspNetCore.Models;

namespace DevApp.ModelSchemas
{
    //[PluralName("ValueWoots")]
    [RequireAuthorization(typeof(Roles), nameof(Roles.EditValues))]
    //[RequireAuthorization(Roles.EditValues)] //Alt syntax, not ideal
    //[KeyType(typeof(int))]
    //[KeyType(typeof(String))]
    //[KeyType(typeof(KeyTestEnum))]
    //[UiControllerName("Admin")]
    public abstract class Value
    {
        [Required]
        [MaxLength(450)]
        //[Display(Name = "WootName")]
        [UiOrder]
        //[DefineNullValueLabel("Any")]
        //[Queryable(required:true, showOnUi:false)]
        //[Queryable]
        //[AbstractOnQuery]
        //[AbstractOnEntity]
        //[AbstractOnInputModel]
        //[AbstractOnViewModel]
        public String Name { get; set; }

        //[Queryable]
        //public int CoolThing { get; set; }

        //[Queryable]
        //public Guid ForeignKey { get; set; }

        //[NoInputModel]
        //[UiOrder]
        //public int NotOnInput { get; set; }

        //[NoEntity]
        //[UiOrder]
        //public int NotOnEntity { get; set; }

        //[NoViewModel]
        //[UiOrder]
        //public int NotOnView { get; set; }
    }
}