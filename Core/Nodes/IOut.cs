using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Nodes
{
    public interface IOut<T> where T : class
    {
       void AddChild(IIn<T> node);
    }
}
