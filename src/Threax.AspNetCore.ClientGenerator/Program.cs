using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace Threax.AspNetCore.ClientGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<String, String> parsedArgs = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                var split = arg.Split('=');
                if (split.Length == 1)
                {
                    parsedArgs.Add(split[0], null);
                }
                if (split.Length > 1)
                {
                    parsedArgs.Add(split[0], split[1]);
                }
            }

            if (parsedArgs.ContainsKey("help"))
            {
                WriteHelp();
            }
            else
            {
                try
                {
                    Task.WaitAll(AsyncMain(parsedArgs));
                }
                catch (AggregateException ex)
                {
                    throw ex.InnerException;
                }
            }
        }

        static void WriteHelp()
        {
            Console.WriteLine("The following arguments can be provided in name=value format.");
            Console.WriteLine("in=path - The path to the swagger doc, can be a local path or a url. This is required.");
            Console.WriteLine("out=path - The path to output file. This is required.");
            Console.WriteLine("client - Include this argument to generate a client.");
            Console.WriteLine("schemas - Include this argument to generate schemas");
        }

        static async Task AsyncMain(Dictionary<String, String> parsedArgs)
        {
            string swaggerDocPath = parsedArgs["in"];
            string outPath = parsedArgs["out"];

            string swaggerData;
            if (swaggerDocPath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    swaggerData = await httpClient.GetStringAsync(swaggerDocPath);
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(File.Open(swaggerDocPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    swaggerData = await sr.ReadToEndAsync();
                }
            }

            String client = null;
            if (parsedArgs.ContainsKey("client"))
            {
                SwaggerToTypeScriptClientCommand Command = new SwaggerToTypeScriptClientCommand();
                //Command.Settings.TypeScriptGeneratorSettings.TemplateFactory = new FetchTemplateFactory(Command.Settings.TypeScriptGeneratorSettings.TemplateFactory);
                Command.OperationGenerationMode = OperationGenerationMode.MultipleClientsFromPathSegments;
                Command.Template = TypeScriptTemplate.Fetch;
                Command.TypeStyle = TypeScriptTypeStyle.Interface;
                Command.Input = SwaggerDocument.FromJson(swaggerData, swaggerDocPath);
                client = await Command.RunAsync();
            }

            String schemas = null;
            if (parsedArgs.ContainsKey("schemas"))
            {
                var swagger = JObject.Parse(swaggerData);
                schemas = $"export var Schemas = {swagger["definitions"]}";
            }

            using (var sr = new StreamWriter(File.Open(outPath, FileMode.Create, FileAccess.Write, FileShare.None)))
            {
                if (client != null)
                {
                    await sr.WriteLineAsync(client);
                }
                if (schemas != null)
                {
                    await sr.WriteLineAsync(schemas);
                }
            }
        }
    }
}
