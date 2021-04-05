using DataTransformer.Core.Config;
using DataTransformer.Core.Plugin;
using DataTransformer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTransformer.Core.Pipeline
{
    /// <inheritdoc />
    public class PipelineFactory : IPipelineFactory
    {
        private readonly IPluginLoader _pluginLoader;
        private readonly IPluginMetadataFactory _pluginMetadataFactory;
        
        public PipelineFactory(IPluginLoader pluginLoader, IPluginMetadataFactory pluginMetadataFactory)
        {
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
            _pluginMetadataFactory = pluginMetadataFactory ?? throw new ArgumentNullException(nameof(pluginMetadataFactory));
        }

        /// <inheritdoc />
        public Models.Pipeline Create(PipelineConfiguration config)
        {
            var pipeline = new Models.Pipeline
            {
                Name = config.Name,
                Plugins = config.Plugins.Select(pluginType => _pluginLoader.LoadPlugin(pluginType)).ToArray()
            };
            pipeline.PluginMetadataMap = CreatePluginMetadataMap(pipeline.Plugins);
            return pipeline;
        }

        private Dictionary<string, PluginMetadata> CreatePluginMetadataMap(IPlugin[] plugins)
        {
            var map = new Dictionary<string, PluginMetadata>();
            foreach (var plugin in plugins)
            {
                var pluginType = plugin.GetType();
                map.Add(pluginType.FullName, _pluginMetadataFactory.Create(pluginType));
            }

            return map;
        }
    }
}
