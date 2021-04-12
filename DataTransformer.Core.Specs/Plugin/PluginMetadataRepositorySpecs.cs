using DataTransformer.Core.Plugin;
using DataTransformer.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DataTransformer.Core.Specs.Plugin.PluginMetadataRepositorySpecs
{
    public abstract class PluginMetadataRepositorySpecsBase : SpecBase<PluginMetadataRepository>
    {
        protected Mock<IPluginMetadataFactory> _pluginMetadataFactory;
        protected Mock<IDictionary<string, PluginMetadata>> _pluginMetadataMap;

        protected PluginMetadata _result;

        protected override void Context()
        {
            _pluginMetadataFactory = Dependency<IPluginMetadataFactory>();
            _pluginMetadataMap = Dependency<IDictionary<string, PluginMetadata>>();
        }

        protected override PluginMetadataRepository CreateSut()
        {
            return new PluginMetadataRepository(_pluginMetadataFactory.Object, _pluginMetadataMap.Object);
        }

        protected class TestPlugin : IPlugin<string, string>
        {
            public string Name => "Test Plugin";

            public Task<string> Decode(string output)
            {
                throw new NotImplementedException();
            }

            public Task<string> Encode(string input)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class WhenToldToGetPluginMetadataForNullPlugin : PluginMetadataRepositorySpecsBase
    {
        private Action _action;

        protected override void Because()
        {
            _action = new Action(() => _result = _sut.Get(null));
        }

        [Fact]
        public void ShouldThrowError()
        {
            var ex = Assert.Throws<ArgumentNullException>(_action);
            Assert.Equal("Value cannot be null. (Parameter 'plugin')", ex.Message);
        }
    }

    public class WhenToldToGetPluginMetadataForARealPluginInCache : PluginMetadataRepositorySpecsBase
    {
        PluginMetadata _pluginMetadata = new();

        protected override void Context()
        {
            base.Context();

            var key = typeof(TestPlugin).AssemblyQualifiedName;
            _pluginMetadataMap.Setup(map => map.ContainsKey(key)).Returns(true);
            _pluginMetadataMap.Setup(map => map[key]).Returns(_pluginMetadata);
        }

        protected override void Because()
        {
            _result = _sut.Get(new TestPlugin());
        }

        [Fact]
        public void ShouldNotCallMetadataFactory()
        {
            _pluginMetadataFactory.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldReturnMetadataFromCache()
        {
            Assert.Equal(_pluginMetadata, _result);
        }
    }

    public class WhenToldToGetPluginMetadataForARealPluginNotInCache : PluginMetadataRepositorySpecsBase
    {
        private PluginMetadata _pluginMetadata = new();

        protected override void Context()
        {
            base.Context();

            _pluginMetadataFactory.Setup(factory => factory.Create(typeof(TestPlugin))).Returns(_pluginMetadata);
        }

        protected override void Because()
        {
            _result = _sut.Get(new TestPlugin());
        }

        [Fact]
        public void ShouldCallMetadataFactory()
        {
            _pluginMetadataFactory.Verify(factory => factory.Create(typeof(TestPlugin)));
        }

        [Fact]
        public void ShouldReturnMetadataReturnedFromFactory()
        {
            Assert.Equal(_pluginMetadata, _result);
        }

        [Fact]
        public void ShouldSaveMetadataInCache()
        {
            var key = typeof(TestPlugin).AssemblyQualifiedName;
            _pluginMetadataMap.VerifySet(map => map[key] = _pluginMetadata);
        }
    }
}
