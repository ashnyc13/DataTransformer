using DataTransformer.Core.Config;
using DataTransformer.Models;

namespace DataTransformer.Core.Factories
{
    /// <summary>
    /// Helps create an instance of <see cref="Pipeline"/>.
    /// </summary>
    public interface IPipelineFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="Pipeline"/>.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        Pipeline Create(PipelineConfiguration config);
    }
}