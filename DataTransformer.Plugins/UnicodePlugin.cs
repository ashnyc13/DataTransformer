using DataTransformer.Models;
using System.Text;
using System.Threading.Tasks;

namespace DataTransformer.Plugins
{
    public class UnicodePlugin : IPlugin<string, byte[]>
    {
        /// <inheritdoc/>
        public string Name => "Unicode";

        /// <inheritdoc/>
        public Task<string> Decode(byte[] input)
        {
            var result = Encoding.Unicode.GetString(input);
            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public Task<byte[]> Encode(string input)
        {
            var result = Encoding.Unicode.GetBytes(input);
            return Task.FromResult(result);
        }
    }
}
