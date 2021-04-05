using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataTransformer.Core.Pipeline
{
    public interface IPipelineService
    {
        Task<IEnumerable<string>> GetAllPipelineNames();
        public Models.Pipeline GetPipelineByName(string pipelineName);
    }
}