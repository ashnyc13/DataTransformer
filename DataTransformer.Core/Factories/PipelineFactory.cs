using DataTransformer.Core.Config;
using DataTransformer.Core.Services;
using DataTransformer.Models;
using System;
using System.Linq;

namespace DataTransformer.Core.Factories
{
    /// <inheritdoc />
    public class PipelineFactory : IPipelineFactory
    {
        private readonly IPluginLoader pluginLoader;

        public PipelineFactory(IPluginLoader pluginService)
        {
            pluginLoader = pluginService ?? throw new ArgumentNullException(nameof(pluginService));
        }

        /// <inheritdoc />
        public Pipeline Create(PipelineConfiguration config)
        {
            return new Pipeline
            {
                Name = config.Name,
                Plugins = config.Plugins.Select(pluginType => pluginLoader.LoadPlugin(pluginType)).ToArray()
            };
        }
    }
}
