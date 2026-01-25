using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using MolluskEngine.GameBoard;

namespace MolluskEngine.Exensions;

public static class EnumExtensions
{
    public static int Count<T>(this T enumValue) where T : struct, System.Enum
    {
        return Enum.GetNames(typeof(T)).Count();
    }
    /*
    public static int Count<T>() where T : struct, Enum
    {
        return Enum.GetNames(typeof(T)).Count();
    }
    */
}
