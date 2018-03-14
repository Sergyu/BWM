using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Nodes
{
    public interface IInitializer<T> : ITicker
    {
        void Initialize(Func<T> initializer);
    }
}
