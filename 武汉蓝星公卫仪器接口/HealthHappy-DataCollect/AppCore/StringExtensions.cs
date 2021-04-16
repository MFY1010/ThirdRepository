using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCore
{
    public static class StringExtensions
    {
        public static string TrimOrNull(this string str)
        {
            return string.IsNullOrEmpty(str) ? "" : str.Trim();
        }
    }
}
