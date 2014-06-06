using System;

namespace CNISS.Bootstraper
{
    public interface IBootstrapperTask<in TContainer>
    {
        Action<TContainer> Task { get; }
    }
}
