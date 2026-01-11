using System;
using Microsoft.Xna.Framework;
using MolluskEngine.Scene;
using MolluskEngine.UI;

namespace MolluskEngine.Menus;

public class TitleMenuNode : Node
{
    public Vector2 Position; // Todo: Shouldn't be hardcoded
    public int Width;
    public int Height;
    public Text Text;
    public TitleMenuNode(Vector2 position, string text, Func<CommandResult>? callback)
    {
        Position = position;
        Text = new Text()
        {
            Content = text,
            Color = Color.Black,
            Position = position,
            Font = "Arial",
        };
        Callback = callback;
        Width = 1000;
        Height = 50;
    }

}
