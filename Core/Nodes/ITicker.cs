using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Nodes
{
    public interface ITicker
    {
        List<ITicker> Tick();
    }
}
