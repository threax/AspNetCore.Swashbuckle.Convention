using Halcyon.HAL.Attributes;
using HateoasTest.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestHalcyonApi.Controllers;
using TestHalcyonApi.Models;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.ViewModels
{
    /// <summary>
    /// A simple test model.
    /// </summary>
    [HalModel]
    [HalSelfActionLink(ThingyController.Rels.Get, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.Get, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.Update, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.Delete, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.ListTestSubData, typeof(ThingyController))]
    [HalActionLink(SubThingyController.Rels.Add, typeof(SubThingyController))]
    [HalActionLink(ThingyController.Rels.AuthorizedProperties, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.RoleProperties, typeof(ThingyController))]
    [DeclareHalLink(ThingyController.Rels.TestDeclareLinkToRel, typeof(ThingyController))]
    public class ThingyView : Thingy
    {
        //Various random test cases, need real tests for these.

        //public List<ComplexObject> ComplexObjects { get; set; }

        //public bool ShowHidden { get; set; }

        //private static readonly Expression<Func<ThingyView, bool>> PropertyToShowExpression = s => s.ShowHidden;
        //[DisplayExpression(nameof(PropertyToShowExpression))]
        //public int PropertyToShow { get; set; }

        //public int? TestNullableInt { get; set; }

        //private static readonly Expression<Func<ThingyView, bool>> DisplayIfNullableInt1 = s => s.TestNullableInt == 1;
        //[DisplayExpression(nameof(DisplayIfNullableInt1))]
        //public int ShowIfTestNullableIntIs1 { get; set; }

        //public enum TestEnum
        //{
        //    On,
        //    Off
        //}

        //public TestEnum? TestNullableDisplayEnum { get; set; }

        //private static readonly Expression<Func<ThingyView, bool>> DisplayIfNullableEnumOn = s => s.TestNullableDisplayEnum == TestEnum.On;
        //[DisplayExpression(nameof(DisplayIfNullableEnumOn))]
        //public int ShowIfNullableTestEnumOn { get; set; }

        //public TestEnum TestDisplayEnum { get; set; }

        //private static readonly Expression<Func<ThingyView, bool>> DisplayIfEnumOn = s => s.TestDisplayEnum == TestEnum.On;
        //[DisplayExpression(nameof(DisplayIfEnumOn))]
        //public int ShowIfTestEnumOn { get; set; }

        //public int Counter { get; set; }

        //private static readonly Expression<Func<ThingyView, bool>> CounterTest = s => s.Counter > 5;
        //[DisplayExpression(nameof(CounterTest))]
        //public int ShowIfCounterBiggerThan5 { get; set; }

        //public String TextValue { get; set; }

        //private static readonly Expression<Func<ThingyView, bool>> ShowIfTextValueIsBeefTest = s => s.TextValue == "Beef";
        //[DisplayExpression(nameof(ShowIfTextValueIsBeefTest))]
        //public int ShowIfTextValueIsBeef { get; set; }
    }
}
