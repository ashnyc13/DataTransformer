using DataTransformer.Core.Config;

namespace DataTransformer.Core.Pipeline
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
        Models.Pipeline Create(PipelineConfiguration config);
    }
}