namespace DataTransformer.DesktopApp
{
    /// <summary>
    /// Helps create an instance of <see cref="PipelineDialog"/>.
    /// </summary>
    public interface IPipelineDialogFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="PipelineDialog"/>.
        /// </summary>
        /// <returns></returns>
        PipelineDialog Create();
    }
}