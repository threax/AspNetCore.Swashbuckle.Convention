using Swashbuckle.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Swashbuckle.Convention
{
    /// <summary>
    /// Options for the Swagger convention.
    /// </summary>
    public class SwaggerConventionOptions
    {
        /// <summary>
        /// The api info for this convention.
        /// </summary>
        public Info ApiInfo { get; set; }

        /// <summary>
        /// True to use the swagger ui. Defaults to true.
        /// </summary>
        public bool UseSwaggerUi { get; set; } = true;

        /// <summary>
        /// The url that swagger starts with, defaults to swagger, applies to both ui and json doc.
        /// </summary>
        /// <remarks>
        /// This seems to be broken in swashbuckle, the swagger doc url won't change, leave this alone for now.
        /// </remarks>
        public String SwaggerUrl { get; private set; } = "swagger";

        internal String CustomBaseUrlPath
        {
            get
            {
                if(SwaggerUrl == null)
                {
                    return "swagger";
                }
                else
                {
                    var moddedUrl = SwaggerUrl;
                    var removeStart = moddedUrl[0] == '\\' || moddedUrl[0] == '/';
                    var removeEnd = moddedUrl.Length > 0 && (moddedUrl[moddedUrl.Length - 1] == '\\' || moddedUrl[moddedUrl.Length - 1] == '/');
                    if (removeStart && removeEnd)
                    {
                        moddedUrl = moddedUrl.Substring(1, moddedUrl.Length - 2);
                    }
                    else
                    {
                        if (removeStart)
                        {
                            moddedUrl = moddedUrl.Substring(1);
                        }
                        if (removeEnd)
                        {
                            moddedUrl = moddedUrl.Substring(0, moddedUrl.Length - 1);
                        }
                    }
                    return moddedUrl;
                }
            }
        }
    }
}
