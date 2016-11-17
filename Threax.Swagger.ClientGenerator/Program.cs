using NJsonSchema.CodeGeneration;
using NJsonSchema.CodeGeneration.TypeScript;
using NSwag;
using NSwag.CodeGeneration.CodeGenerators;
using NSwag.CodeGeneration.CodeGenerators.TypeScript;
using NSwag.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Threax.Swagger.ClientGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.WaitAll(AsyncMain(args));

        }

        static async Task AsyncMain(String[] args)
        { 
            string documentPath = args[0];
            string swaggerData;

            if (args.Length > 1)
            {
                swaggerData = args[1];
            }
            else
            {
                if(documentPath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        swaggerData = await httpClient.GetStringAsync(documentPath);
                    }
                }
                else
                {
                    using(StreamReader sr = new StreamReader(File.Open(documentPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    {
                        swaggerData = await sr.ReadToEndAsync();
                    }
                }
            }

            SwaggerToTypeScriptClientCommand Command = new SwaggerToTypeScriptClientCommand();
            Command.Settings.TypeScriptGeneratorSettings.TemplateFactory = new FetchTemplateFactory(Command.Settings.TypeScriptGeneratorSettings.TemplateFactory);
            Command.OperationGenerationMode = OperationGenerationMode.MultipleClientsFromPathSegments;
            Command.Template = TypeScriptTemplate.Fetch;
            Command.TypeStyle = TypeScriptTypeStyle.Interface;
            Command.Input = SwaggerDocument.FromJson(swaggerData, documentPath);
            var code = await Command.RunAsync();

            using (var sr = new StreamWriter(File.Open("test.ts", FileMode.Create, FileAccess.Write, FileShare.None)))
            {
                await sr.WriteAsync(code);
            }
        }
    }
}
