using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DataTransformer.Core
{
    /// <inheritdoc />
    public class TypeFinder : ITypeFinder
    {
        /// <inheritdoc />
        public IEnumerable<T> FindAndCreateInstances<T>(string directory)
        {
            var plugins = new List<T>();
            if (Directory.Exists(directory))
            {
                var dllFiles = Directory.GetFiles(directory, "*.dll");
                foreach (var dllFile in dllFiles)
                {
                    byte[] bytes = File.ReadAllBytes(dllFile);
                    var assembly = Assembly.Load(bytes);
                    var aPlugins = CreateInstancesFromAssembly<T>(assembly);
                    if (aPlugins != null)
                    {
                        plugins.AddRange(aPlugins);
                    }
                }
            }
            return plugins;
        }

        private static IEnumerable<T> CreateInstancesFromAssembly<T>(Assembly assembly)
        {
            var plugins = new List<T>();
            if (assembly != null)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsInterface || type.IsAbstract)
                    {
                        continue;
                    }
                    else
                    {
                        if (type.GetInterface(typeof(T).FullName) != null)
                        {
                            plugins.Add((T)Activator.CreateInstance(type));
                        }
                    }
                }
            }
            return plugins;
        }
    }
}
