using System;

namespace DataTransformer.Models
{
    public class Pipeline
    {
        /// <summary>
        /// A list of plugins that make up the pipeline.
        /// </summary>
        public IPlugin[] Plugins { get; set; }
        
        /// <summary>
        /// Name of the pipeline.
        /// </summary>
        public string Name { get; set; }
    }
}
