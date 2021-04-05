using DataTransformer.Models;

namespace DataTransformer.Core.Plugin
{
    public interface IPluginMetadataRepository
    {
        PluginMetadata Get(IPlugin plugin);
    }
}