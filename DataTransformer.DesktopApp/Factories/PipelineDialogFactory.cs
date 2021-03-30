using System;
using Microsoft.Extensions.DependencyInjection;

namespace DataTransformer.DesktopApp.Factories
{
    /// <inheritdoc />
    public class PipelineDialogFactory : IPipelineDialogFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PipelineDialogFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <inheritdoc />
        public PipelineDialog Create() => _serviceProvider.GetService<PipelineDialog>();
    }
}
