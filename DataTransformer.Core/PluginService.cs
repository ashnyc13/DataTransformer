using DataTransformer.Models;
using DataTransformer.Plugins;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DataTransformer.Core
{
    public class PluginService : IPluginService
    {
        private readonly ITypeFinder _typeFinder;

        public PluginService(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder ?? throw new System.ArgumentNullException(nameof(typeFinder));
        }

        public Task<IPlugin[]> LoadAllPlugins()
        {
            var pluginPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var allPlugins = _typeFinder.FindAndCreateInstances<IPlugin>(pluginPath).ToArray();
            return Task.FromResult(allPlugins);
        }
    }
}
