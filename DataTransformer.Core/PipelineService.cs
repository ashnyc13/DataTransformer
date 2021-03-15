using DataTransformer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTransformer.Core
{
    public class PipelineService : IPipelineService
    {
        private readonly List<Pipeline> _allPipelines;
        private readonly IPluginService _pluginService;

        public event EventHandler<PipelineProgressEventArgs> Progress;

        public PipelineService(IPluginService pluginService)
        {
            _allPipelines = new List<Pipeline>
            {
                new Pipeline { Name = "Base64+Ascii encode" }
            };
            _pluginService = pluginService ?? throw new ArgumentNullException(nameof(pluginService));
        }

        public Task<IEnumerable<string>> GetAllPipelineNames()
        {
            return Task.FromResult(_allPipelines.Select(pipeline => pipeline.Name));
        }

        public async Task<string> Execute(string pipelineName, string text)
        {
            // get the pipeline by name
            var pipeline = _allPipelines.FirstOrDefault(pipeline => pipeline.Name == pipelineName);

            // get plugins
            if(pipeline.Plugins == null)
            {
                pipeline.Plugins = await _pluginService.LoadAllPlugins();
            }

            // execute the plugins under the pipeline
            object inputOutput = text;
            var length = pipeline.Plugins.Length;
            var progress = 0;
            for (int i = 0; i < length; i++)
            {
                // Signal that we're running the plugin
                var plugin = pipeline.Plugins[i];
                if (Progress != null) Progress(this, new PipelineProgressEventArgs
                {
                    PercentProgress = progress,
                    StatusMessage = $"Running plugin '{plugin.Name}'..."
                });

                // Run plugin
                inputOutput = await plugin.Encode(inputOutput);

                // Update progress
                progress = ((i + 1) * 100) / length;
                
                // TODO: validate the output goes
                // in as input in the next plugin
            }

            // TODO: validate final output is a string

            // Signal all done.
            if (Progress != null) Progress(this, new PipelineProgressEventArgs
            {
                PercentProgress = 0,
                StatusMessage = $"Ready."
            });
            return inputOutput.ToString();
        }
    }
}
