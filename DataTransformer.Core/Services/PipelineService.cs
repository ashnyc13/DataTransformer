using DataTransformer.Core.Config;
using DataTransformer.Core.Factories;
using DataTransformer.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTransformer.Core.Services
{
    public class PipelineService : IPipelineService
    {
        private readonly List<Pipeline> _allPipelines = new();
        private readonly IPipelineFactory _pipelineFactory;

        public event EventHandler<PipelineProgressEventArgs> Progress;

        public PipelineService(IPipelineFactory pipelineFactory, IOptions<LibraryConfiguration> libraryConfig)
        {
            _pipelineFactory = pipelineFactory ?? throw new ArgumentNullException(nameof(pipelineFactory));

            if (libraryConfig is null)
            {
                throw new ArgumentNullException(nameof(libraryConfig));
            }
            CreatePipelines(libraryConfig.Value);
        }

        public Task<IEnumerable<string>> GetAllPipelineNames()
        {
            return Task.FromResult(_allPipelines.Select(pipeline => pipeline.Name));
        }

        public async Task<string> Execute(string pipelineName, string text)
        {
            // get the pipeline by name
            var pipeline = _allPipelines.FirstOrDefault(pipeline => pipeline.Name == pipelineName);

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
                inputOutput = await ExecuteEncode(pipeline, plugin, inputOutput);

                // Update progress
                progress = (i + 1) * 100 / length;

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

        private void CreatePipelines(LibraryConfiguration config)
        {
            if (config is null) throw new ArgumentNullException(nameof(config));
            foreach (var pipelineConfig in config.Pipelines)
            {
                _allPipelines.Add(_pipelineFactory.Create(pipelineConfig));
            }
        }

        private Task<object> ExecuteEncode(Pipeline pipeline, IPlugin plugin, object input)
        {
            var pluginMetadata = pipeline.PluginMetadataMap[plugin.GetType().FullName];
            var task = pluginMetadata.EncodeFunction.Invoke(plugin, new[] { input }) as Task;
            return task.ContinueWith(t => {
                dynamic dynT = t;
                return dynT.Result as object;
            });
        }
    }
}
