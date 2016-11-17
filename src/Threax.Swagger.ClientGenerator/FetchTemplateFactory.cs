using NJsonSchema.CodeGeneration;
using NSwag.CodeGeneration.CodeGenerators.TypeScript.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threax.Swagger.ClientGenerator
{
    class FetchTemplateFactory : ITemplateFactory
    {
        ITemplateFactory fallback;

        public FetchTemplateFactory(ITemplateFactory fallback)
        {
            this.fallback = fallback;
        }

        public ITemplate CreateTemplate(string package, string template, object model)
        {
            //var templateModel = new ClientTemplateModel()
            if (package == "TypeScript" && template == "FetchClient")
            {
                return new FetchClientTemplate((ClientTemplateModel)model);
            }
            else
            {
                return fallback.CreateTemplate(package, template, model);
            }
        }
    }
}
