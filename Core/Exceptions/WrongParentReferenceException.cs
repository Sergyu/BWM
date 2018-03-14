using Core.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class WrongParentReferenceException : BaseCoreException
    {
        public WrongParentReferenceException(string msg, IIdentity node)  : base(msg, node) { }
    }
}
