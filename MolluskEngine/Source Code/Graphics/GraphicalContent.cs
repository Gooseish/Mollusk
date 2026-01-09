using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MolluskEngine.Graphics;

public static class GraphicalContent
{
    // Fields
    private static Dictionary<string, Texture2D> menuTextures = new();
    private static Dictionary<string, SpriteFont> fonts = new();

    // Accessors
    public static IReadOnlyDictionary<string, Texture2D> MenuTextures {get{return menuTextures;}}
    public static IReadOnlyDictionary<string, SpriteFont> Fonts {get{return fonts;}}

    public static void Initialize()
    {
        menuTextures["WhiteSquare"] = TextureFromSize(1, 1);
    }
    public static void LoadContent(ContentManager content)
    {
        fonts["Arial"] = content.Load<SpriteFont>(@"Fonts/Arial");
    }

    // Private Controls
    private static Texture2D TextureFromSize(int width, int height)
    {
        Texture2D texture = new Texture2D(Core.GraphicsDevice, width, height);
        Color[] data = new Color[width * height];
        for (int n = 0; n < data.Length; n++)
        {
            data[n] = Color.White;
        }
        texture.SetData(data);
        return texture;
    }
}
