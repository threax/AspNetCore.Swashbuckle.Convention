using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Model
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
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [MaxLength(25, ErrorMessage = "Name cannot be greater than 25 characters")]
        [Required(ErrorMessage = "You must include a name")]
        public String Name { get; set; }
    }
}
