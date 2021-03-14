using DataTransformer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataTransformer.Core
{
    public interface IPipelineService
    {
        Task<IEnumerable<string>> GetAllPipelineNames();
        Task<string> Execute(string pipelineName, string text);

        event EventHandler<PipelineProgressEventArgs> Progress;
    }
}