using System;
using System.IO;
using System.Reflection;

namespace Threax.NetCore.FileCopier
{
    public class Copier
    {
        private string[] resourceNames;
        private Assembly assembly;

        public Copier(Type t)
        {
            assembly = t.GetTypeInfo().Assembly;
            resourceNames = assembly.GetManifestResourceNames();
        }

        /// <summary>
        /// Copy files from the internal namespace to the specified folder, will only copy a bare folder.
        /// .net will usually embed a file in a namespace that starts with the project name and 
        /// </summary>
        /// <param name="baseNamespace"></param>
        /// <param name="outDir"></param>
        public void CopyResourceDirectory(string baseNamespace, string outDir, CopierOptions options = null)
        {
            if(options == null)
            {
                options = CopierOptions.Default;
            }

            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            foreach (var name in resourceNames)
            {
                int baseIndex;
                if (options.UseLastIndex)
                {
                    baseIndex = name.LastIndexOf(baseNamespace);
                }
                else
                {
                    baseIndex = name.IndexOf(baseNamespace);
                }
                
                if(baseIndex == -1)
                {
                    continue;
                }

                baseIndex += baseNamespace.Length;

                var fileName = name.Substring(baseIndex);
                Console.WriteLine(fileName);
                using (var stream = File.Open(Path.Combine(outDir, fileName), FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (var assemblyStream = assembly.GetManifestResourceStream(name))
                    {
                        assemblyStream.CopyTo(stream);
                    }
                }
            }
        }
    }
}
