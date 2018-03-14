using Core.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class TreeBuilderException : BaseCoreException
    {
        public TreeBuilderException(string message, IIdentity node) : 
            base(message, node)
        {

        }
    }
}
