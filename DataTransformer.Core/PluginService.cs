using DataTransformer.Models;
using DataTransformer.Plugins;
using System.Threading.Tasks;

namespace DataTransformer.Core
{
    public class PluginService : IPluginService
    {
        public Task<IPlugin[]> LoadAllPlugins()
        {
            // TODO: go through each assembly in the executing folder
            // and find the plugin assembly. Then load all eligible plugins.
            var allPlugins = new IPlugin[] { new AsciiPlugin(), new Base64Plugin() };
            return Task.FromResult(allPlugins);
        }
    }
}
