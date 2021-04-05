using DataTransformer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataTransformer.Core.Services
{
    public interface IPipelineService
    {
        Task<IEnumerable<string>> GetAllPipelineNames();
        public Pipeline GetPipelineByName(string pipelineName);
    }
}