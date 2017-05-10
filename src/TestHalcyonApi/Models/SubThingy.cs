using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestHalcyonApi.Models
{
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
    }
}
