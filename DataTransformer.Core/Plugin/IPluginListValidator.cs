using DataTransformer.Models;
using System.Collections.Generic;

namespace DataTransformer.Core.Plugin
{
    public interface IPluginListValidator
    {
        PluginListValidationResult Validate(IEnumerable<IPlugin> plugins);
    }
}