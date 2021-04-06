using DataTransformer.Core.Config;
using DataTransformer.Core.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTransformer.Core.Pipeline
{
    public class PipelineRepository : IPipelineRepository
    {
        private readonly IPipelineFactory _pipelineFactory;
        private readonly IConfigDbContext _dbContext;
        private readonly IOptions<LibraryConfiguration> _libraryConfig;

        public PipelineRepository(IPipelineFactory pipelineFactory, IConfigDbContext dbContext, IOptions<LibraryConfiguration> libraryConfig)
        {
            _pipelineFactory = pipelineFactory ?? throw new ArgumentNullException(nameof(pipelineFactory));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _libraryConfig = libraryConfig ?? throw new ArgumentNullException(nameof(libraryConfig));
        }

        public Task<IEnumerable<string>> GetAllPipelineNames()
        {
            var pipelineNames = _libraryConfig.Value.Pipelines.Select(pipelineConfig => pipelineConfig.Name);
            return Task.FromResult(pipelineNames);
        }

        public Task<Models.Pipeline> GetPipelineByName(string pipelineName)
        {
            var pipelineConfig = _libraryConfig.Value.Pipelines.FirstOrDefault(pipelineConfig => pipelineConfig.Name == pipelineName);
            var pipeline = _pipelineFactory.Create(pipelineConfig);
            return Task.FromResult(pipeline);
        }

        public async Task SavePipeline(Models.Pipeline pipeline)
        {
            if (pipeline is null) throw new ArgumentNullException(nameof(pipeline));

            // Find pipeline config by name or create new
            var config = _libraryConfig.Value;
            var pipelineConfig = config.Pipelines.FirstOrDefault(
                config => config.Name.Equals(pipeline.Name, StringComparison.OrdinalIgnoreCase));
            if (pipelineConfig == null)
            {
                pipelineConfig = new PipelineConfiguration();
                config.Pipelines = config.Pipelines.Append(pipelineConfig).ToArray();
            }

            // Set fields
            pipelineConfig.Name = pipeline.Name;
            pipelineConfig.Plugins = pipeline.Plugins.Select(plugin => plugin.GetType().AssemblyQualifiedName).ToArray();

            // Save data
            await _dbContext.Save(config);
        }
    }
}
