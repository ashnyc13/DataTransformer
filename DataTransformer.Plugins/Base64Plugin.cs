using DataTransformer.Models;
using System;
using System.Threading.Tasks;

namespace DataTransformer.Plugins
{
    public class Base64Plugin : IPlugin
    {
        /// <inheritdoc/>
        public string Name => "Base 64";

        /// <inheritdoc/>
        public Task<object> Decode(object input)
        {
            if (input is not string) throw new ArgumentException($"Input must be a string", nameof(input));
            object result = Convert.FromBase64String(input as string);
            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public Task<object> Encode(object input)
        {
            if (input is not byte[]) throw new ArgumentException($"Input must be a byte[]", nameof(input));
            object result = Convert.ToBase64String(input as byte[]);
            return Task.Delay(1000).ContinueWith(task => result);
        }
    }
}
