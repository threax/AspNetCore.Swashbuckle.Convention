using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext.ValueProviders;
using Threax.AspNetCore.Models;

namespace TestHalcyonApi.Models
{
    [NullValueLabel("None")]
    public enum TestEnum
    {
        [Display(Name = "Test Value 1")]
        TestValue1,
        [Display(Name = "Test Value 2")]
        TestValue2,
        [Display(Name = "Test Value 3")]
        TestValue3,
    }

    public class SubThingy
    {
        [UiOrder]
        public int SubThingyId { get; set; }

        [UiOrder]
        public decimal Amount { get; set; }

        [UiOrder]
        public int ThingyId { get; set; }

        [UiOrder]
        public TestEnum EnumTest { get; set; }

        [UiOrder]
        public TestEnum? EnumTestNullable { get; set; }

        [UiOrder]
        [NullValueLabel("Relabeled")]
        public TestEnum? EnumTestNullableRelabel { get; set; }
    }
}
