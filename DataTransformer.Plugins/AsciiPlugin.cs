using DataTransformer.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DataTransformer.Plugins
{
    public class AsciiPlugin : IPlugin
    {
        /// <inheritdoc/>
        public string Name => "Ascii";

        /// <inheritdoc/>
        public Task<object> Decode(object input)
        {
            if (input is not byte[]) throw new ArgumentException($"Input must be a byte[]", nameof(input));
            object result = Encoding.ASCII.GetString(input as byte[]);
            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public Task<object> Encode(object input)
        {
            if (input is not string) throw new ArgumentException($"Input must be a string", nameof(input));
            object result = Encoding.ASCII.GetBytes(input as string);
            return Task.Delay(1000).ContinueWith(task => result);
        }
    }
}
