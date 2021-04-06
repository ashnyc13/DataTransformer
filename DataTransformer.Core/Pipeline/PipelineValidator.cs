using DataTransformer.Core.Plugin;
using DataTransformer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataTransformer.Core.Pipeline
{
    public class PipelineValidator : IPipelineValidator
    {
        private readonly IPluginListValidator _pluginListValidator;
        private readonly IPipelineRepository _pipelineRepository;

        public PipelineValidator(IPluginListValidator pluginListValidator, IPipelineRepository pipelineRepository)
        {
            _pluginListValidator = pluginListValidator ?? throw new System.ArgumentNullException(nameof(pluginListValidator));
            _pipelineRepository = pipelineRepository ?? throw new System.ArgumentNullException(nameof(pipelineRepository));
        }

        public async Task<PipelineValidationResult> Validate(Models.Pipeline pipeline)
        {
            // Validate if name is empty
            if (string.IsNullOrWhiteSpace(pipeline?.Name))
            {
                return new PipelineValidationResult
                {
                    IsValid = false,
                    Error = "Pipeline name cannot be empty."
                };
            }

            // Validate plugins list
            var result = _pluginListValidator.Validate(pipeline.Plugins);
            if(!result.IsValid)
            {
                return new PipelineValidationResult
                {
                    IsValid = result.IsValid,
                    Error = result.Error
                };
            }

            // Validate if name exists
            var allPipelineNames = await _pipelineRepository.GetAllPipelineNames();
            if(allPipelineNames.Contains(pipeline.Name, StringComparer.OrdinalIgnoreCase))
            {
                return new PipelineValidationResult
                {
                    IsValid = true,
                    Warning = "Pipeline name already exists."
                };
            }

            return PipelineValidationResult.CreateValid();
        }
    }
}
