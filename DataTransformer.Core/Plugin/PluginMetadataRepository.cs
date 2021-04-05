using DataTransformer.Models;
using System;
using System.Collections.Generic;

namespace DataTransformer.Core.Plugin
{
    public class PluginMetadataRepository : IPluginMetadataRepository
    {
        /// <summary>
        /// Plugin metadata associated with plugin type name as key.
        /// </summary>
        private readonly Dictionary<string, PluginMetadata> _pluginMetadataMap = new();

        private readonly IPluginMetadataFactory _pluginMetadataFactory;

        public PluginMetadataRepository(IPluginMetadataFactory pluginMetadataFactory)
        {
            _pluginMetadataFactory = pluginMetadataFactory ?? throw new ArgumentNullException(nameof(pluginMetadataFactory));
        }

        public PluginMetadata Get(IPlugin plugin)
        {
            if (plugin is null) throw new ArgumentNullException(nameof(plugin));

            var pluginType = plugin.GetType();
            var pluginTypeName = pluginType.AssemblyQualifiedName;
            if (_pluginMetadataMap.ContainsKey(pluginTypeName)) return _pluginMetadataMap[pluginTypeName];

            return _pluginMetadataMap[pluginTypeName] = _pluginMetadataFactory.Create(pluginType);
        }
    }
}
