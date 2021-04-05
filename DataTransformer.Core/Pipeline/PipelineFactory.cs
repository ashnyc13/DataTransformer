using DataTransformer.Core.Config;
using DataTransformer.Core.Plugin;
using System;
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
            var pipeline = new Models.Pipeline
            {
                Name = config.Name,
                Plugins = config.Plugins.Select(pluginType => _pluginLoader.LoadPlugin(pluginType)).ToArray()
            };
            return pipeline;
        }
    }
}
