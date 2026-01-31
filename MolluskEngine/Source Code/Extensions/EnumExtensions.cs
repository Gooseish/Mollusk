using System;
using System.Collections.Generic;
using System.Linq;

namespace MolluskEngine.Extensions;

public static class EnumExtensions
{
    extension<T>(T) where T : Enum
    {
        public static int Count()
        {
            return Enum.GetNames(typeof(T)).Length;
        }
        public static string[] Names()
        {
            return Enum.GetNames(typeof(T));
        }
        public static IEnumerable<T> Values()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
