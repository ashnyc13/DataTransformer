using System.Collections.Generic;

namespace DataTransformer.Core.Utility
{
    /// <summary>
    /// Helps find concrete objects of given interface
    /// type by looking for assemblies that contain them.
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>
        /// Creates concrete instances using the types found
        /// that implement the given interface type.
        /// </summary>
        /// <typeparam name="T">Interface type.</typeparam>
        /// <param name="directory"></param>
        /// <returns></returns>
        IEnumerable<T> FindAndCreateInstances<T>(string directory);
    }
}