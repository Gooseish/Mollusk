using System;
using System.Collections.Generic;

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
    }
}
