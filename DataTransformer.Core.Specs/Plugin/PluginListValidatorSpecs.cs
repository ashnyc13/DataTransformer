using DataTransformer.Core.Plugin;
using DataTransformer.Models;
using Moq;
using System;
using System.Collections.Generic;
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
        private IPlugin _pluginInstace;

        protected override void Context()
        {
            base.Context();

            _pluginInstace = Dependency<IPlugin<string, string>>().Object;

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

    public class WhenAskedToValidateAListWithAPluginThatTakesByteArrayAndReturnsString : PluginListValidatorSpecsBase
    {
        private Mock<IPlugin> _pluginInstace;

        protected override void Context()
        {
            base.Context();

            _pluginInstace = Dependency<IPlugin<byte[], string>>().As<IPlugin>();
            _pluginInstace.SetupGet(plugin => plugin.Name).Returns("Plugin");

            _inputPluginsList = new[] { _pluginInstace.Object };
            _pluginMetadataRepository.Setup(factory => factory.Get(_pluginInstace.Object)).Returns(new PluginMetadata
            {
                InputType = typeof(byte[]),
                OutputType = typeof(string)
            });
        }

        [Fact]
        public void ShouldReturnInValid()
        {
            Assert.NotNull(_result);
            Assert.False(_result.IsValid);
            Assert.Equal("First plugin in the list must have input type as string. 'Plugin' plugin instead accepts a 'System.Byte[]'.", _result.Error);
        }
    }

    public class WhenAskedToValidateAListWithAPluginThatTakesStringAndReturnsByteArray : PluginListValidatorSpecsBase
    {
        private Mock<IPlugin> _pluginInstace;

        protected override void Context()
        {
            base.Context();

            _pluginInstace = Dependency<IPlugin<string, byte[]>>().As<IPlugin>();
            _pluginInstace.SetupGet(plugin => plugin.Name).Returns("Plugin");

            _inputPluginsList = new[] { _pluginInstace.Object };
            _pluginMetadataRepository.Setup(factory => factory.Get(_pluginInstace.Object)).Returns(new PluginMetadata
            {
                InputType = typeof(string),
                OutputType = typeof(byte[])
            });
        }

        [Fact]
        public void ShouldReturnInValid()
        {
            Assert.NotNull(_result);
            Assert.False(_result.IsValid);
            Assert.Equal("Last plugin in the list must have output type as string. 'Plugin' plugin instead outputs a 'System.Byte[]'.", _result.Error);
        }
    }

    public class WhenAskedToValidateAValidListOfPlugins : PluginListValidatorSpecsBase
    {
        private IPlugin _firstPlugin;
        private IPlugin _secondPlugin;

        protected override void Context()
        {
            base.Context();

            _firstPlugin = Dependency<IPlugin<string, byte[]>>().Object;
            _secondPlugin = Dependency<IPlugin<byte[], string>>().Object;

            _inputPluginsList = new[] { _firstPlugin, _secondPlugin };
            _pluginMetadataRepository.Setup(factory => factory.Get(_firstPlugin)).Returns(new PluginMetadata
            {
                InputType = typeof(string),
                OutputType = typeof(byte[])
            });
            _pluginMetadataRepository.Setup(factory => factory.Get(_secondPlugin)).Returns(new PluginMetadata
            {
                InputType = typeof(byte[]),
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

    public class WhenAskedToValidateAnInValidListOfPlugins : PluginListValidatorSpecsBase
    {
        private Mock<IPlugin> _firstPlugin;
        private Mock<IPlugin> _secondPlugin;

        protected override void Context()
        {
            base.Context();

            _firstPlugin = Dependency<IPlugin<string, byte[]>>().As<IPlugin>();
            _firstPlugin.SetupGet(plugin => plugin.Name).Returns("First plugin");
            _secondPlugin = Dependency<IPlugin<string, string>>().As<IPlugin>();
            _secondPlugin.SetupGet(plugin => plugin.Name).Returns("Second plugin");
            
            _inputPluginsList = new[] { _firstPlugin.Object, _secondPlugin.Object };
            _pluginMetadataRepository.Setup(factory => factory.Get(_firstPlugin.Object)).Returns(new PluginMetadata
            {
                InputType = typeof(string),
                OutputType = typeof(byte[])
            });
            _pluginMetadataRepository.Setup(factory => factory.Get(_secondPlugin.Object)).Returns(new PluginMetadata
            {
                InputType = typeof(string),
                OutputType = typeof(string)
            });
        }

        [Fact]
        public void ShouldReturnInValid()
        {
            Assert.NotNull(_result);
            Assert.False(_result.IsValid);
            Assert.Equal("Output type of plugin 'First plugin' must be the same as input type of 'Second plugin'.", _result.Error);
        }
    }
}
