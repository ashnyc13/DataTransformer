namespace DataTransformer.Core.Config
{
    /// <summary>
    /// Represents the configuration needed
    /// to create a pipeline instance.
    /// </summary>
    public class PipelineConfiguration
    {
        public string Name { get; set; }
        public string[] Plugins { get; set; }
    }
}
