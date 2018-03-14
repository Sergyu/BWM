using Core.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class WrongNodeConfigurationException : BaseCoreException
    {
        public WrongNodeConfigurationException(string msg, IIdentity node)
            : base(msg, node) { }
    }
}
