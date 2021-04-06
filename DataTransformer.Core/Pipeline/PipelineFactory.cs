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
        
        public PipelineFactory(IPluginLoader pluginLoader)
        {
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
        }

        /// <inheritdoc />
        public Models.Pipeline Create(PipelineConfiguration config)
        {
            return Create(config.Name, config.Plugins.Select(pluginType => _pluginLoader.LoadPlugin(pluginType)));
        }

        /// <inheritdoc />
        public Models.Pipeline Create(string name, IEnumerable<IPlugin> plugins)
        {
            return new Models.Pipeline
            {
                Name = name,
                Plugins = plugins?.ToArray()
            };
        }

        /// <inheritdoc />
        public Models.Pipeline CreateNew()
        {
            return Create(null, null);
        }
    }
}
