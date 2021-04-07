using DataTransformer.Models;
using System;
using System.Threading.Tasks;

namespace DataTransformer.Plugins
{
    public class Base64Plugin : IPlugin<byte[], string>
    {
        /// <inheritdoc/>
        public string Name => "Base 64";

        /// <inheritdoc/>
        public Task<byte[]> Decode(string output)
        {
            var result = Convert.FromBase64String(output);
            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public Task<string> Encode(byte[] input)
        {
            var result = Convert.ToBase64String(input);
            return Task.FromResult(result);
        }
    }
}
