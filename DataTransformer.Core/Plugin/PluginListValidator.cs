using DataTransformer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTransformer.Core.Plugin
{
    public class PluginListValidator : IPluginListValidator
    {
        private readonly IPluginMetadataRepository _pluginMetadataRepository;

        public PluginListValidator(IPluginMetadataRepository pluginMetadataRepository)
        {
            _pluginMetadataRepository = pluginMetadataRepository ?? throw new ArgumentNullException(nameof(pluginMetadataRepository));
        }

        public PluginListValidationResult Validate(IEnumerable<IPlugin> plugins)
        {
            // Empty list is considered valid
            var pluginsArray = plugins.ToArray();
            if (!pluginsArray.Any()) return PluginListValidationResult.CreateValid();

            // First plugin should always accept string
            var plugin = pluginsArray.First();
            var metadata = _pluginMetadataRepository.Get(plugin);
            if (metadata.InputType != typeof(string))
            {
                return new PluginListValidationResult
                {
                    IsValid = false,
                    Error = $"First plugin in the list must have input type as string. '{plugin.Name}' plugin instead accepts a '{metadata.InputType.FullName}'."
                };
            }

            // Last plugin should always output string
            plugin = pluginsArray.Last();
            metadata = _pluginMetadataRepository.Get(plugin);
            if (metadata.OutputType != typeof(string))
            {
                return new PluginListValidationResult
                {
                    IsValid = false,
                    Error = $"Last plugin in the list must have output type as string. '{plugin.Name}' plugin instead outputs a '{metadata.OutputType.FullName}'."
                };
            }

            // Go through the list to confirm
            // that the plugins are lined up with their input
            // and output types
            if (pluginsArray.Length > 1)
            {
                for (int i = 0; i < pluginsArray.Length - 1; i++)
                {
                    plugin = pluginsArray[i];
                    metadata = _pluginMetadataRepository.Get(plugin);
                    var nextPlugin = pluginsArray[i + 1];
                    var nextPluginMetadata = _pluginMetadataRepository.Get(nextPlugin);

                    // Make sure the current plugin's output lines up
                    // with the input of the next plugin
                    if (metadata.OutputType != nextPluginMetadata.InputType)
                    {
                        return new PluginListValidationResult
                        {
                            IsValid = false,
                            Error = $"Output type of plugin '{plugin.Name}' must be the same as input type of '{nextPlugin.Name}'."
                        };
                    }
                }
            }

            // Everything is valid
            return PluginListValidationResult.CreateValid();
        }
    }
}
