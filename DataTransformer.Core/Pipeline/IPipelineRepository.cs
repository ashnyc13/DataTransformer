using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataTransformer.Core.Pipeline
{
    public interface IPipelineRepository
    {
        Task<IEnumerable<string>> GetAllPipelineNames();
        public Models.Pipeline GetPipelineByName(string pipelineName);
        void SavePipeline(Models.Pipeline pipeline);
    }
}