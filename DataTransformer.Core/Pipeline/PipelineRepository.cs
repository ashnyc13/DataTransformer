using DataTransformer.Core.Config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTransformer.Core.Pipeline
{
    public class PipelineRepository : IPipelineRepository
    {
        private readonly List<Models.Pipeline> _allPipelines = new();
        private readonly IPipelineFactory _pipelineFactory;

        public PipelineRepository(IPipelineFactory pipelineFactory, IOptions<LibraryConfiguration> libraryConfig)
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

        public Models.Pipeline GetPipelineByName(string pipelineName)
        {
            return _allPipelines.FirstOrDefault(pipeline => pipeline.Name == pipelineName);
        }

        public void SavePipeline(Models.Pipeline pipeline)
        {
            throw new NotImplementedException();
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
