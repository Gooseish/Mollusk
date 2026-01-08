using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MolluskEngine.Scene;

namespace MolluskEngine.Scene;

public class SceneTitle : _Scene
{
    private SceneMenu menu = new();

    public override void Draw(GraphicsDevice graphicsDevice)
    {
        
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
