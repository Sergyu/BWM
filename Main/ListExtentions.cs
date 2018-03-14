using System;
using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public static class ListExtentions
    {
        public static bool IsNullOrEmpty(this ICollection lst)
        {
            return null == lst || lst.Count == 0;
        }

        public static List<T> ToList<T>(this T item)
        {
            var lst = new List<T>();
            lst.Add(item);
            return lst;
        }

        public static IList<T> ForEach<T>(this IList<T> lst, Action<T> action)
        {
            foreach (T t in lst)
            {
                action(t);
            }
            return lst;
        }

        public static IList<T> AddMany<T>(this IList<T> lst, params T[] toBeAddedLst)
        {
            return toBeAddedLst.ForEach(i => lst.Add(i));
        }

    }
}
