using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class Singleton<T> where T : class, ISingleton, new()
    {
        private static Dictionary<string, object> _instances = new Dictionary<string, object>();
        public static T Instance
        {
            get
            {
                string synchronizer = typeof(T).FullName;
                T instance = _instances.ContainsKey(synchronizer) ? _instances[synchronizer] as T : null;
                if (null == instance)
                {
                    lock (synchronizer)
                    {
                        instance = _instances.ContainsKey(synchronizer) ? _instances[synchronizer] as T : null;
                        if (null == instance)
                        {
                            instance = new T();
                            _instances.Add(synchronizer, instance);
                        }
                    }
                }
                return instance;
            }
        }
    }
}
