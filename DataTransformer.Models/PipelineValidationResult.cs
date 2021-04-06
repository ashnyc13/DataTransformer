namespace DataTransformer.Models
{
    /// <summary>
    /// Result of a pipeline validation process.
    /// </summary>
    public class PipelineValidationResult
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }
        public string Warning { get; set; }

        public static PipelineValidationResult CreateValid()
        {
            return new PipelineValidationResult { IsValid = true };
        }
    }
}
