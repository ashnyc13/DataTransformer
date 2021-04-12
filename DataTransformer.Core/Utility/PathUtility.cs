using System.IO;
using System.Reflection;

namespace DataTransformer.Core.Utility
{
    public class PathUtility : IPathUtility
    {
        public string GetExecutionDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
