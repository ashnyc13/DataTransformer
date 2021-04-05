using DataTransformer.Core.Config;
using DataTransformer.Core.Services;
using DataTransformer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataTransformer.Core.Factories
{
    /// <inheritdoc />
    public class PipelineFactory : IPipelineFactory
    {
        private readonly IPluginLoader pluginLoader;
        private static readonly Type GenericPluginType = typeof(IPlugin<,>);

        public PipelineFactory(IPluginLoader pluginService)
        {
            pluginLoader = pluginService ?? throw new ArgumentNullException(nameof(pluginService));
        }

        /// <inheritdoc />
        public Pipeline Create(PipelineConfiguration config)
        {
            var pipeline = new Pipeline
            {
                Name = config.Name,
                Plugins = config.Plugins.Select(pluginType => pluginLoader.LoadPlugin(pluginType)).ToArray()
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
                map.Add(pluginType.FullName, CreatePluginMetadata(pluginType));
            }

            return map;
        }

        private static PluginMetadata CreatePluginMetadata(Type pluginType)
        {
            var interaces = pluginType.GetInterfaces();
            if (!interaces.Any(iface => iface.IsGenericType && iface.GetGenericTypeDefinition() == GenericPluginType))
                throw new ArgumentException($"Given plugin type '{pluginType.FullName}' doesn't implement the generic IPlugin<> interface.",
                    nameof(pluginType));

            var metadata = new PluginMetadata
            {
                DecodeFunction = pluginType.GetMethod("Decode", BindingFlags.Public | BindingFlags.Instance),
                EncodeFunction = pluginType.GetMethod("Encode", BindingFlags.Public | BindingFlags.Instance)
            };
            metadata.InputType = metadata.EncodeFunction.GetParameters()[0].ParameterType;
            metadata.OutputType = metadata.DecodeFunction.GetParameters()[0].ParameterType;

            return metadata;
        }
    }
}
