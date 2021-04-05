using DataTransformer.Models;
using System;
using System.Linq;
using System.Reflection;

namespace DataTransformer.Core.Plugin
{
    public class PluginMetadataFactory : IPluginMetadataFactory
    {
        private static readonly Type GenericPluginType = typeof(IPlugin<,>);

        public PluginMetadata Create(Type pluginType)
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
