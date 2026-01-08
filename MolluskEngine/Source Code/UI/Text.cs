using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MolluskEngine.Graphics;

namespace MolluskEngine.UI;

public class Text
{
    public string Font;
    public string Content;
    public Vector2 Position;
    public Color Color;
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(
            GraphicalContent.Fonts[Font],
            Content,
            Position,  // Should have a vector extension that points to the center of the game window
            Color
            );
    }
}
