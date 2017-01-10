using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema.CodeGeneration;
using NJsonSchema.CodeGeneration.TypeScript;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Threax.AspNetCore.ClientGenerator
{
    class ClientGeneratorException : Exception
    {
        public ClientGeneratorException(String message)
            :base(message)
        {

        }
    }

    class Program
    {
        static int Main(string[] args)
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
                    try
                    {
                        Task.WaitAll(AsyncMain(parsedArgs));
                    }
                    catch (AggregateException ex)
                    {
                        throw ex.InnerException;
                    }
                }
                catch (ClientGeneratorException ex)
                {
                    Console.WriteLine(ex.Message);
                    return 1;
                }
            }
            return 0;
        }

        static void WriteHelp()
        {
            Console.WriteLine("Tool Current Working dir " + Path.GetFullPath("."));
            Console.WriteLine("The following arguments can be provided in name=value format.");
            Console.WriteLine("in=path - The path to the swagger doc, can be a local path or a url. This is required.");
            Console.WriteLine("out=path - The path to output file. This is required.");
            Console.WriteLine("client - Include this argument to generate a client.");
            Console.WriteLine("schemas - Include this argument to generate schemas");
            Console.WriteLine("generationMode=MultipleClientsFromOperationId | MultipleClientsFromPathSegments | SingleClientFromOperationId - Set the client generation mode to one of these options. Defaults to MultipleClientsFromOperationId");
        }

        static async Task AsyncMain(Dictionary<String, String> parsedArgs)
        {
            string swaggerDocPath;
            if (!parsedArgs.TryGetValue("in", out swaggerDocPath))
            {
                throw new ClientGeneratorException("You must provide a in=path arugment that provides the path to the swagger doc (url or local path).");
            }
            string outPath;
            if(!parsedArgs.TryGetValue("out", out outPath))
            {
                throw new ClientGeneratorException("You must provide a out=path arugment that provides the path to the output file, must be a typescript (.ts) file.");
            }

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
                //var generationMode = OperationGenerationMode.MultipleClientsFromOperationId;
                //if (parsedArgs.ContainsKey("generationMode"))
                //{
                //    generationMode = (OperationGenerationMode)Enum.Parse(typeof(OperationGenerationMode), parsedArgs["generationMode"]);
                //}

                //SwaggerToTypeScriptClientCommand Command = new SwaggerToTypeScriptClientCommand();
                //Command.Settings.TypeScriptGeneratorSettings.TemplateFactory = new FetchTemplateFactory(Command.Settings.TypeScriptGeneratorSettings.TemplateFactory);
                //Command.OperationGenerationMode = generationMode;
                //Command.Template = TypeScriptTemplate.Fetch;
                //Command.TypeStyle = TypeScriptTypeStyle.Interface;
                //Command.Input = await SwaggerDocument.FromJsonAsync(swaggerData, swaggerDocPath);
                //client = await Command.RunAsync();
                client = "//client generation disabled";
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
