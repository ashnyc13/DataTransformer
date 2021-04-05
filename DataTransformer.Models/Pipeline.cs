using System;
using System.Collections.Generic;

namespace DataTransformer.Models
{
    public class Pipeline
    {
        /// <summary>
        /// Name of the pipeline.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A list of plugins that make up the pipeline.
        /// </summary>
        public IPlugin[] Plugins { get; set; }

        /// <summary>
        /// Plugin metadata associated with plugin type name as key.
        /// </summary>
        public Dictionary<string, PluginMetadata> PluginMetadataMap { get; set; }
    }
}
