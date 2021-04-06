using DataTransformer.Core.Config;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace DataTransformer.Core.Data
{
    public class ConfigDbContext : IConfigDbContext
    {
        public async Task Save(LibraryConfiguration libraryConfig)
        {
            var configJson = JsonConvert.SerializeObject(libraryConfig, Formatting.Indented);

            var currentFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var configFileLocation = Path.Combine(currentFolder, "appsettings.json");
            await File.WriteAllTextAsync(configFileLocation, configJson);
        }
    }
}
