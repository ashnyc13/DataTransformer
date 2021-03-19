using DataTransformer.Models;
using System.Threading.Tasks;

namespace DataTransformer.Core.Services
{
    /// <summary>
    /// Helps load plugins
    /// </summary>
    public interface IPluginLoader
    {
        /// <summary>
        /// Loads a list of all discoverable plugins.
        /// </summary>
        /// <returns></returns>
        Task<IPlugin[]> LoadAllPlugins();

        /// <summary>
        /// Loads the given plugin by it's type name
        /// </summary>
        /// <param name="pluginType"></param>
        /// <returns></returns>
        IPlugin LoadPlugin(string pluginType);
    }
}