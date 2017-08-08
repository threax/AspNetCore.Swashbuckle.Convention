using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.Customizers
{
    /// <summary>
    /// This doesn't do aything useful to the schema, but it does test that the extensions work
    /// </summary>
    public class PointlessSchemaCustomizer : ISchemaCustomizer
    {
        public Task Customize(SchemaCustomizerArgs args)
        {
            if (args.SchemaProperty.ExtensionData == null)
            {
                args.SchemaProperty.ExtensionData = new Dictionary<String, Object>();
            }
            args.SchemaProperty.ExtensionData.Add("x-pointless-addition", DateTime.Now.ToString());

            return Task.FromResult(0);
        }
    }
}
