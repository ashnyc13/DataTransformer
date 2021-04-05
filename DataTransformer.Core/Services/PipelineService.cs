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

        public Pipeline GetPipelineByName(string pipelineName)
        {
            return _allPipelines.FirstOrDefault(pipeline => pipeline.Name == pipelineName);
        }

        private void CreatePipelines(LibraryConfiguration config)
        {
            if (config is null) throw new ArgumentNullException(nameof(config));
            foreach (var pipelineConfig in config.Pipelines)
            {
                _allPipelines.Add(_pipelineFactory.Create(pipelineConfig));
            }
        }
    }
}
