using DataTransformer.Models;
using System.Text;
using System.Threading.Tasks;

namespace DataTransformer.Plugins
{
    public class AsciiPlugin : IPlugin<string, byte[]>
    {
        /// <inheritdoc/>
        public string Name => "Ascii";

        /// <inheritdoc/>
        public Task<string> Decode(byte[] input)
        {
            var result = Encoding.ASCII.GetString(input);
            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public Task<byte[]> Encode(string input)
        {
            var result = Encoding.ASCII.GetBytes(input);
            return Task.Delay(1000).ContinueWith(task => result);
        }
    }
}
