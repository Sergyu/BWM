using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class StringUtils
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return str == null || str == string.Empty;
        }
    }
}
