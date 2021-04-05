using DataTransformer.Models;
using System;
using System.Threading.Tasks;

namespace DataTransformer.Core.Services
{
    public class PipelineExecuter : IPipelineExecuter
    {
        private readonly IPipelineService _pipelineService;

        public event EventHandler<PipelineProgressEventArgs> Progress;

        public PipelineExecuter(IPipelineService pipelineService)
        {
            _pipelineService = pipelineService ?? throw new ArgumentNullException(nameof(pipelineService));
        }

        public async Task<string> Execute(string pipelineName, string text)
        {
            // get the pipeline by name
            var pipeline = _pipelineService.GetPipelineByName(pipelineName);

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

        private Task<object> ExecuteEncode(Pipeline pipeline, IPlugin plugin, object input)
        {
            var pluginMetadata = pipeline.PluginMetadataMap[plugin.GetType().FullName];
            var task = pluginMetadata.EncodeFunction.Invoke(plugin, new[] { input }) as Task;
            return task.ContinueWith(t =>
            {
                dynamic dynT = t;
                return dynT.Result as object;
            });
        }
    }
}
