using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public abstract class Model<T> where T : class
    {
        public T Payload
        {
            get;
            protected set;
        }

        protected abstract void Initialize();
    }
}
