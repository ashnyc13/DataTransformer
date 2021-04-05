using DataTransformer.Models;
using System;
using System.Threading.Tasks;

namespace DataTransformer.Core.Services
{
    public interface IPipelineExecuter
    {
        event EventHandler<PipelineProgressEventArgs> Progress;
        Task<string> Execute(string pipelineName, string text);
    }
}