using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Halcyon.HAL.Attributes;
using Threax.AspNetCore.Halcyon.Ext;
using Threax.AspNetCore.Halcyon.Ext.UIAttrs;
namespace DevApp.Models 
{
    public interface IValue 
    {

        String Name { get; set; }

    }

    public interface IValueId
    {
        Guid ValueId { get; set; }
    }    

    public interface IValueQuery
    {
        Guid? ValueId { get; set; }
    }
}