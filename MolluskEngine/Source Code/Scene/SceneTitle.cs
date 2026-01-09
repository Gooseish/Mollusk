using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MolluskEngine.Menus;
using MolluskEngine.UI;

namespace MolluskEngine.Scene;

public class SceneTitle : _Scene
{
    private SceneMenu menu = new();
    private SceneTitleInputHandler InputHandler;
    private Text titleScreenText;

    public SceneTitle()
    {
        Initialize();
    }

    public void Initialize()
    {
        titleScreenText = new Text() // Could be more data-driven
        {
            Font = "Arial",
            Content = "Press Start",
            Position = new Vector2(600, 300),
            Color = Color.Black,
        };

        InputHandler = new SceneTitleInputHandler(this);
    }

    public override void Draw(GraphicsDevice graphicsDevice)
    {
        SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);

        if (menu.MenuActive)
        {
            spriteBatch.Begin();
            menu.Draw(spriteBatch);
            spriteBatch.End();
            return;
        }

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
        
        InputHandler.Update();
    }

    public CommandResult OpenMenu()
    {
        menu.OpenMenu<TitleMenu>();
        return CommandResult.Accepted;
    }
}
