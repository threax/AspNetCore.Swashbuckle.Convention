using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext.ValueProviders;

namespace TestHalcyonApi.Models
{
    [NullEnumLabel("None")]
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
        public int SubThingyId { get; set; }

        public decimal Amount { get; set; }

        public int ThingyId { get; set; }

        public TestEnum EnumTest { get; set; }

        public TestEnum? EnumTestNullable { get; set; }

        [NullEnumLabel("Relabeled")]
        public TestEnum? EnumTestNullableRelabel { get; set; }
    }
}
