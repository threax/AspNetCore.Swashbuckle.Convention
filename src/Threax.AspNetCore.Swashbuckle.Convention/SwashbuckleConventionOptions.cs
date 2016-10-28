using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Swashbuckle.Convention
{
    /// <summary>
    /// Additional options for the Swashbuckle convention.
    /// </summary>
    public class SwashbuckleConventionOptions
    {
        /// <summary>
        /// Set this to true to add a header called bearer to all api requests.
        /// Defaults to true.
        /// </summary>
        public bool HasJwtBearerAuth { get; set; } = true;
    }
}
