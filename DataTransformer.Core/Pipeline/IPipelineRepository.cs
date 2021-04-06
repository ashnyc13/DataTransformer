using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataTransformer.Core.Pipeline
{
    public interface IPipelineRepository
    {
        Task<IEnumerable<string>> GetAllPipelineNames();
        public Task<Models.Pipeline> GetPipelineByName(string pipelineName);
        Task SavePipeline(Models.Pipeline pipeline);
    }
}