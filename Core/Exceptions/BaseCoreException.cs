using Core.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public abstract class BaseCoreException : Exception
    {
        public BaseCoreException(string message, IIdentity node) : 
            base(string.Format("{0}, node: {1}", message, node.ToString()))
        {

        }
    }
}
