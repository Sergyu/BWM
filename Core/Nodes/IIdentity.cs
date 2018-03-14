using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Nodes
{
    public interface IIdentity
    {
        string Name { get; }
        int Id { get; }
    }
}
