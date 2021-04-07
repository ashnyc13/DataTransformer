using DataTransformer.Models;
using System;
using System.Threading.Tasks;
using System.Web;

namespace DataTransformer.Plugins
{
    public class JavaScriptStringEncodePlugin : IPlugin<string, string>
    {
        /// <inheritdoc/>
        public string Name => "JavaScriptEncode";

        /// <inheritdoc/>
        public Task<string> Decode(string input)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<string> Encode(string input)
        {
            return Task.FromResult(HttpUtility.JavaScriptStringEncode(input));
        }
    }
}
