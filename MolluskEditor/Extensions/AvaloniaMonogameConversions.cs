using System;

namespace MolluskEditor.Extensions;

public static class AvaloniaMonogameConversions
{
    public static Avalonia.Media.Color ToAvaloniaColor(
        this Microsoft.Xna.Framework.Color source)
    {
        return new Avalonia.Media.Color(source.A, source.R, source.G, source.B);
    }
    public static Microsoft.Xna.Framework.Color ToMonogameColor(
        this Avalonia.Media.Color source)
    {
        return new Microsoft.Xna.Framework.Color(source.R, source.G, source.B, source.A);
    }
    public static Avalonia.Media.Color ShallowCopy(
        this Avalonia.Media.Color source)
    {
        return new Avalonia.Media.Color(source.A, source.R, source.G, source.B);
    }
    public static Microsoft.Xna.Framework.Color ShallowCopy(
        this Microsoft.Xna.Framework.Color source)
    {
        return new Microsoft.Xna.Framework.Color(source.R, source.G, source.B, source.A);
    }
}
