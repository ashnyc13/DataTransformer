using DataTransformer.Core.Plugin;
using DataTransformer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataTransformer.Core.Pipeline
{
    public class PipelineExecuter : IPipelineExecuter
    {
        private readonly IPipelineRepository _pipelineService;
        private readonly IPluginMetadataRepository _pluginMetadataRepository;

        public event EventHandler<PipelineProgressEventArgs> Progress;

        public PipelineExecuter(IPipelineRepository pipelineService, IPluginMetadataRepository pluginMetadataRepository)
        {
            _pipelineService = pipelineService ?? throw new ArgumentNullException(nameof(pipelineService));
            _pluginMetadataRepository = pluginMetadataRepository ?? throw new ArgumentNullException(nameof(pluginMetadataRepository));
        }

        public async Task<string> Execute(string pipelineName, string input)
        {
            // get the pipeline by name
            var pipeline = await _pipelineService.GetPipelineByName(pipelineName);

            // execute the plugins under the pipeline
            var plugins = pipeline.Plugins;
            return await ExecutePluginsList(input, plugins, false);
        }

        public async Task<string> ExecuteReverse(string pipelineName, string input)
        {
            // get the pipeline by name
            var pipeline = await _pipelineService.GetPipelineByName(pipelineName);

            // Reverse plugins list
            var plugins = pipeline.Plugins;
            plugins = plugins.Reverse().ToArray();

            // execute the plugins list
            return await ExecutePluginsList(input, plugins, true);
        }

        private async Task<string> ExecutePluginsList(string input, IPlugin[] plugins, bool isReversed)
        {
            object inputOutput = input;
            var length = plugins.Length;
            var progress = 0;
            for (int i = 0; i < length; i++)
            {
                // Signal that we're running the plugin
                var plugin = plugins[i];
                if (Progress != null) Progress(this, new PipelineProgressEventArgs
                {
                    PercentProgress = progress,
                    StatusMessage = $"Running plugin '{plugin.Name}'..."
                });

                // Run plugin
                inputOutput = await ExecutePluginFunction(plugin, inputOutput, isReversed);

                // Update progress
                progress = (i + 1) * 100 / length;
            }

            // Signal all done.
            if (Progress != null) Progress(this, new PipelineProgressEventArgs
            {
                PercentProgress = 0,
                StatusMessage = $"Ready."
            });
            return inputOutput.ToString();
        }

        private Task<object> ExecutePluginFunction(IPlugin plugin, object input, bool isReversed)
        {
            var pluginMetadata = _pluginMetadataRepository.Get(plugin);
            var pluginFunction = isReversed ? pluginMetadata.DecodeFunction : pluginMetadata.EncodeFunction;
            var task = pluginFunction.Invoke(plugin, new[] { input }) as Task;
            return task.ContinueWith(t =>
            {
                dynamic dynT = t;
                return dynT.Result as object;
            });
        }
    }
}
