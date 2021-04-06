using DataTransformer.Core.Config;
using DataTransformer.Models;
using System.Collections.Generic;

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

        /// <summary>
        /// Creates an instance of <see cref="Pipeline"/>.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        Models.Pipeline Create(string name, IEnumerable<IPlugin> plugins);

        /// <summary>
        /// Creates a default instance of <see cref="Pipeline"/>.
        /// </summary>
        /// <returns></returns>
        Models.Pipeline CreateNew();
    }
}