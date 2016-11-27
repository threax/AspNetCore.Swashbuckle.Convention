using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.ExceptionToJson
{
    /// <summary>
    /// The options for ExceptionToJson.
    /// </summary>
    public class ExceptionToJsonOptions
    {
        /// <summary>
        /// True to enable detailed errors.
        /// </summary>
        public bool DetailedErrors { get; set; } = false;

        /// <summary>
        /// If this is set to true any exceptions that generate an ObjectResult will null their exceptions
        /// allowing ResultFilters to run.
        /// </summary>
        public bool AllowResultFilters { get; set; } = true;

        /// <summary>
        /// Order for the filter
        /// </summary>
        public int Order { get; set; }
    }
}
