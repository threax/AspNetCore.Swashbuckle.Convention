using Halcyon.HAL.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestHalcyonApi.Models
{
    /// <summary>
    /// A simple test model.
    /// </summary>
    public class Thingy
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Thingy()
        {

        }

        /// <summary>
        /// Id, it is important to fully name it here and in routes to avoid naming collisions.
        /// </summary>
        public int ThingyId { get; set; }

        /// <summary>
        /// Name, provides some test data.
        /// </summary>
        [MaxLength(25, ErrorMessage = "Name cannot be greater than 25 characters")]
        [Required(ErrorMessage = "You must include a name")]
        public String Name { get; set; }
    }
}
