using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Nodes
{
    public interface IIn<T> : ITicker where T : class
    {
        void In(Token<T> token);

        IList<T> GetPayloads();
    }
}
