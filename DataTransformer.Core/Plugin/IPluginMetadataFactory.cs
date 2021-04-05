using DataTransformer.Models;
using System;

namespace DataTransformer.Core.Plugin
{
    public interface IPluginMetadataFactory
    {
        PluginMetadata Create(Type pluginType);
    }
}