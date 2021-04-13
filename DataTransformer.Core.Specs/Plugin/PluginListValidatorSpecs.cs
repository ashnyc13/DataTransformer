using DataTransformer.Core.Plugin;
using DataTransformer.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DataTransformer.Core.Specs.Plugin.PluginListValidatorSpecs
{
    public class PluginListValidatorSpecsBase : SpecBase<PluginListValidator>
    {
        protected Mock<IPluginMetadataRepository> _pluginMetadataRepository;
        protected IEnumerable<IPlugin> _inputPluginsList;
        protected PluginListValidationResult _result;

        protected override void Context()
        {
            _pluginMetadataRepository = Dependency<IPluginMetadataRepository>();
        }

        protected override PluginListValidator CreateSut()
        {
            return new PluginListValidator(_pluginMetadataRepository.Object);
        }

        protected override void Because()
        {
            _result = _sut.Validate(_inputPluginsList);
        }
    }
    public class WhenAskedToValidateAnEmptyList : PluginListValidatorSpecsBase
    {
        protected override void Context()
        {
            base.Context();

            _inputPluginsList = Array.Empty<IPlugin>();
        }

        [Fact]
        public void ShouldReturnValid()
        {
            Assert.NotNull(_result);
            Assert.True(_result.IsValid);
        }
    }

    public class WhenAskedToValidateAListWithAPluginThatTakesStringAndReturnsString : PluginListValidatorSpecsBase
    {
        private class TestPlugin : IPlugin<string, string>
        {
            public string Name => throw new NotImplementedException();

            public Task<string> Decode(string output)
            {
                throw new NotImplementedException();
            }

            public Task<string> Encode(string input)
            {
                throw new NotImplementedException();
            }
        }

        private IPlugin _pluginInstace = new TestPlugin();

        protected override void Context()
        {
            base.Context();

            _inputPluginsList = new[] { _pluginInstace };
            _pluginMetadataRepository.Setup(factory => factory.Get(_pluginInstace)).Returns(new PluginMetadata
            {
                InputType = typeof(string),
                OutputType = typeof(string)
            });
        }

        [Fact]
        public void ShouldReturnValid()
        {
            Assert.NotNull(_result);
            Assert.True(_result.IsValid);
        }
    }
}
