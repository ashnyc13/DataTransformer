using System;
using System.Reflection;

namespace DataTransformer.Models
{
    /// <summary>
    /// Contains metadata associated with a given
    /// plugin type for fast execution.
    /// </summary>
    public class PluginMetadata
    {
        public Type InputType { get; set; }
        public Type OutputType { get; set; }
        public MethodInfo EncodeFunction { get; set; }
        public MethodInfo DecodeFunction { get; set; }
    }
}