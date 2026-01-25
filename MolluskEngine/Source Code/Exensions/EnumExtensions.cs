using System;
using System.Linq;

namespace MolluskEngine.Exensions;

public static class EnumExtensions
{
    extension<T>(T) where T : Enum
    {
        public static int Count()
        {
            return Enum.GetNames(typeof(T)).Length;
        }
    }
}
