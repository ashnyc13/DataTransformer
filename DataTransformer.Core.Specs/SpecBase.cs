using Moq;
using System.Threading.Tasks;

namespace DataTransformer.Core.Specs
{
    public abstract class SpecBase<T>
    {
        protected readonly T _sut;

        protected SpecBase()
        {
            Context();
            _sut = CreateSut();
            Because();
        }

        protected abstract void Context();
        protected abstract T CreateSut();
        protected abstract void Because();

        protected Mock<TDependency> Dependency<TDependency>() where TDependency : class
        {
            return new Mock<TDependency>();
        }

        protected TResult RunSync<TResult>(Task<TResult> task)
        {
            return task.GetAwaiter().GetResult();
        }
    }
}