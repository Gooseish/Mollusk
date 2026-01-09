using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MolluskEngine.Menus;
using MolluskEngine.Source_Code.Scene.InputHandlers;

namespace MolluskEngine.Scene;

public class SceneMenu : ISceneComponent
{
    public bool MenuActive; // Should be getter only
    public bool InspectActive;
    public SceneMenuInputHandler InputHandler;

    public void OpenMenu<MenuType>() 
    {
        MenuActive = true;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        
    }

    public void Update(GameTime gameTime)
    {
        
    }
}
