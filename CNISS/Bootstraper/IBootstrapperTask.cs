using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNISS.Bootstraper
{
    public interface IBootstrapperTask<in TContainer>
    {
        Action<TContainer> Task { get; }
    }
}
