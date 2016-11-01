using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Model
{
    public class Thingy
    {
        public Thingy()
        {

        }

        public int Id { get; set; }

        [MaxLength(25, ErrorMessage = "Name cannot be greater than 25 characters")]
        [Required(ErrorMessage = "You must include a name")]
        public String Name { get; set; }
    }
}
