using DataTransformer.Core.Plugin;
using DataTransformer.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DataTransformer.Core.Specs.Plugin.PluginMetadataFactorySpecs
{
    public abstract class PluginMetadataFactorySpecsBase : SpecBase<PluginMetadataFactory>
    {
        protected PluginMetadata _result;

        protected override void Context()
        {
        }

        protected override PluginMetadataFactory CreateSut()
        {
            return new PluginMetadataFactory();
        }
    }

    public class WhenToldToCreateMetadataForAValidPluginType : PluginMetadataFactorySpecsBase
    {
        protected override void Because()
        {
            _result = _sut.Create(typeof(TestPlugin));
        }

        [Fact]
        public void ShouldNotReturnNull()
        {
            Assert.NotNull(_result);
        }

        [Fact]
        public void ShouldSetInputTypeCorrectly()
        {
            Assert.Equal(typeof(TestInput), _result.InputType);
        }

        [Fact]
        public void ShouldSetOutputTypeCorrectly()
        {
            Assert.Equal(typeof(TestOutput), _result.OutputType);
        }

        [Fact]
        public void ShouldSetEncodeFunctionCorrectly()
        {
            Assert.Equal(typeof(IPlugin<TestInput, TestOutput>).GetMethod("Encode"), _result.EncodeFunction);
        }

        [Fact]
        public void ShouldSetDecodeFunctionCorrectly()
        {
            Assert.Equal(typeof(IPlugin<TestInput, TestOutput>).GetMethod("Decode"), _result.DecodeFunction);
        }


        private class TestInput
        {

        }

        private class TestOutput
        {

        }

        private class TestPlugin : IPlugin<TestInput, TestOutput>
        {
            public string Name => "Test Plugin";

            public Task<TestInput> Decode(TestOutput output)
            {
                throw new System.NotImplementedException();
            }

            public Task<TestOutput> Encode(TestInput input)
            {
                throw new System.NotImplementedException();
            }
        }
    }

    public class WhenToldToCreateMetadataForAnInvalidPluginType : PluginMetadataFactorySpecsBase
    {
        private Action _testAction;

        protected override void Because()
        {
            _testAction = new Action(() => _result = _sut.Create(typeof(InvalidPlugin)));
        }

        [Fact]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentException>(_testAction);
            Assert.Equal($"Given plugin type '{typeof(InvalidPlugin).FullName}' doesn't implement the generic IPlugin<> interface. (Parameter 'pluginType')", ex.Message);
        }

        private class InvalidPlugin
        {

        }
    }
}
