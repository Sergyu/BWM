using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class IdentityGenerator : ISingleton
    {
        private int _identity = 0;
        private static object _synchronizer = new object();

        public object Sinhronizer => _synchronizer;

        public int GetNewId()
        {
            return ++_identity;
        }
    }
}
