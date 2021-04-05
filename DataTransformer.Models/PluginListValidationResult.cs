using System;

namespace DataTransformer.Models
{
    public class PluginListValidationResult
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }

        public static PluginListValidationResult CreateValid()
        {
            return new PluginListValidationResult { IsValid = true };
        }
    }
}
