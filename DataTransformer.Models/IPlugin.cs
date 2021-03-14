using System.Threading.Tasks;

namespace DataTransformer.Models
{
    public interface IPlugin
    {
        /// <summary>
        /// Name of the plugin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Encodes the given <paramref name="input"/>.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<object> Encode(object input);

        /// <summary>
        /// Decodes the given <paramref name="input"/>.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<object> Decode(object input);
    }
}