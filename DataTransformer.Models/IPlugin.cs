using System.Threading.Tasks;

namespace DataTransformer.Models
{
    public interface IPlugin
    {
        /// <summary>
        /// Name of the plugin.
        /// </summary>
        string Name { get; }
    }

    public interface IPlugin<TInput, TOutput> : IPlugin
    {
        /// <summary>
        /// Encodes the given <paramref name="input"/>.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<TOutput> Encode(TInput input);

        /// <summary>
        /// Decodes the given <paramref name="output"/>.
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public Task<TInput> Decode(TOutput output);
    }
}