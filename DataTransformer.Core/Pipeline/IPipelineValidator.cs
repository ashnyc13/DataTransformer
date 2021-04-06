using DataTransformer.Models;
using System.Threading.Tasks;

namespace DataTransformer.Core.Pipeline
{
    public interface IPipelineValidator
    {
        Task<PipelineValidationResult> Validate(Models.Pipeline pipeline);
    }
}