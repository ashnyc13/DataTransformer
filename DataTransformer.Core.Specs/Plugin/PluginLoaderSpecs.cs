using DataTransformer.Core.Plugin;
using DataTransformer.Core.Utility;
using DataTransformer.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace DataTransformer.Core.Specs.Plugin.PluginLoaderSpecs
{
    public class WhenToldToLoadAllPlugins : SpecBase<PluginLoader>
    {
        private Mock<ITypeFinder> _typeFinder;
        private Mock<IPathUtility> _pathUtility;

        private IPlugin[] _result;

        private const string _pathUtilityOutput = "Current Execution Path";
        private IEnumerable<IPlugin> _typeFinderOutput = Array.Empty<IPlugin>();

        protected override void Context()
        {
            _typeFinder = Dependency<ITypeFinder>();
            _pathUtility = Dependency<IPathUtility>();

            _pathUtility.Setup(util => util.GetExecutionDirectory()).Returns(_pathUtilityOutput);
            _typeFinder.Setup(finder => finder.FindAndCreateInstances<IPlugin>(_pathUtilityOutput)).Returns(_typeFinderOutput);
        }
        protected override PluginLoader CreateSut()
        {
            return new PluginLoader(_typeFinder.Object, _pathUtility.Object);
        }

        protected override void Because()
        {
            _result = RunSync(_sut.LoadAllPlugins());
        }

        [Fact]
        public void ShouldAskPathUtilityToGetExecutionDirectory()
        {
            _pathUtility.Verify(util => util.GetExecutionDirectory());
        }

        [Fact]
        public void ShouldAskTypeFinderToFindAndCreateInstances()
        {
            _typeFinder.Verify(finder => finder.FindAndCreateInstances<IPlugin>(_pathUtilityOutput));
        }

        [Fact]
        public void ShouldReturnAllPlugins()
        {
            Assert.Equal(_result, _typeFinderOutput);
        }
    }
}
