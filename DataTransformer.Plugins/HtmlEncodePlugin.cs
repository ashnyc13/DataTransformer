using DataTransformer.Models;
using System.Threading.Tasks;
using System.Web;

namespace DataTransformer.Plugins
{
    public class HtmlEncodePlugin : IPlugin<string, string>
    {
        /// <inheritdoc/>
        public string Name => "HtmlEncode";

        /// <inheritdoc/>
        public Task<string> Decode(string input)
        {
            return Task.FromResult(HttpUtility.HtmlDecode(input));
        }

        /// <inheritdoc/>
        public Task<string> Encode(string input)
        {
            return Task.FromResult(HttpUtility.HtmlEncode(input));
        }
    }
}
