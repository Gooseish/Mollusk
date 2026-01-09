using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MolluskEngine.Scene;
using MolluskEngine.UI;

namespace MolluskEngine.Scene;

public class SceneTitle : _Scene
{
    private SceneMenu menu = new();
    private Text titleScreenText = new()
    {
        Font = "Arial",
        Content = "Press Start",
        Position = new Vector2(600, 300),
        Color = Color.Black,
    };

    public override void Draw(GraphicsDevice graphicsDevice)
    {
        SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);

        spriteBatch.Begin();
        titleScreenText.Draw(spriteBatch);
        spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        if (menu.MenuActive)
        {
            menu.Update(gameTime);
            return;
        }
        if (Global.Input.GetKeyState(Input.CommandName.Start).KeyPressed)
            menu.MenuActive = true;
    }
}
