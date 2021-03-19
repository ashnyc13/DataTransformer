using DataTransformer.Core.Utility;
using DataTransformer.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DataTransformer.Core.Services
{
    /// <inheritdoc />
    public class PluginLoader : IPluginLoader
    {
        private readonly ITypeFinder _typeFinder;

        public PluginLoader(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder ?? throw new System.ArgumentNullException(nameof(typeFinder));
        }

        /// <inheritdoc />
        public Task<IPlugin[]> LoadAllPlugins()
        {
            var pluginPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var allPlugins = _typeFinder.FindAndCreateInstances<IPlugin>(pluginPath).ToArray();
            return Task.FromResult(allPlugins);
        }

        /// <inheritdoc />
        public IPlugin LoadPlugin(string pluginType)
        {
            return Activator.CreateInstance(Type.GetType(pluginType)) as IPlugin;
        }
    }
}
