using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class WrongChildPayloadTypeException : Exception
    {
        public WrongChildPayloadTypeException(string msg) : base(msg) { }
    }
}
