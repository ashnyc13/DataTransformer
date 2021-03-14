using DataTransformer.Models;
using System.Threading.Tasks;

namespace DataTransformer.Core
{
    public interface IPluginService
    {
        /// <summary>
        /// Loads a list of all discoverable plugins.
        /// </summary>
        /// <returns></returns>
        Task<IPlugin[]> LoadAllPlugins();
    }
}