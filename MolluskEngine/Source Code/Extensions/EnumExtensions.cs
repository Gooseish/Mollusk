using System;

namespace MolluskEngine.Extensions;

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
