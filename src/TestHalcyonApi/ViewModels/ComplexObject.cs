using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestHalcyonApi.ViewModels
{
    public class ComplexObject
    {
        [MaxLength(25, ErrorMessage = "Name cannot be greater than 25 characters")]
        [Required(ErrorMessage = "You must include a name")]
        public String Name { get; set; }

        public int Number { get; set; }
    }
}
