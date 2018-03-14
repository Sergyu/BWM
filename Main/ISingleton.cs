using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface ISingleton
    {
        /// <summary>
        /// Gets a thread synchronziation object
        /// </summary>
        object Sinhronizer { get; }

    }
}
