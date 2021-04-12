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
        private readonly IDictionary<string, PluginMetadata> _pluginMetadataMap;

        private readonly IPluginMetadataFactory _pluginMetadataFactory;

        public PluginMetadataRepository(IPluginMetadataFactory pluginMetadataFactory) :
            this(pluginMetadataFactory, new Dictionary<string, PluginMetadata>())
        {
        }

        internal protected PluginMetadataRepository(IPluginMetadataFactory pluginMetadataFactory,
            IDictionary<string, PluginMetadata> pluginMetadataMap)
        {
            _pluginMetadataFactory = pluginMetadataFactory ?? throw new ArgumentNullException(nameof(pluginMetadataFactory));
            _pluginMetadataMap = pluginMetadataMap ?? throw new ArgumentNullException(nameof(pluginMetadataMap));
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
