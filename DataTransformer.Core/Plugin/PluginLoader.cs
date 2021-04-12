using DataTransformer.Core.Utility;
using DataTransformer.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DataTransformer.Core.Plugin
{
    /// <inheritdoc />
    public class PluginLoader : IPluginLoader
    {
        private readonly ITypeFinder _typeFinder;
        private readonly IPathUtility _pathUtility;

        public PluginLoader(ITypeFinder typeFinder, IPathUtility pathUtility)
        {
            _typeFinder = typeFinder ?? throw new System.ArgumentNullException(nameof(typeFinder));
            _pathUtility = pathUtility ?? throw new ArgumentNullException(nameof(pathUtility));
        }

        /// <inheritdoc />
        public Task<IPlugin[]> LoadAllPlugins()
        {
            var pluginPath = _pathUtility.GetExecutionDirectory();
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
