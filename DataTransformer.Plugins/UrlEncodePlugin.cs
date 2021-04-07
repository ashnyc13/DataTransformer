using DataTransformer.Models;
using System.Threading.Tasks;
using System.Web;

namespace DataTransformer.Plugins
{
    public class UrlEncodePlugin : IPlugin<string, string>
    {
        /// <inheritdoc/>
        public string Name => "UrlEncode";

        /// <inheritdoc/>
        public Task<string> Decode(string input)
        {
            return Task.FromResult(HttpUtility.UrlDecode(input));
        }

        /// <inheritdoc/>
        public Task<string> Encode(string input)
        {
            return Task.FromResult(HttpUtility.UrlEncode(input));
        }
    }
}
