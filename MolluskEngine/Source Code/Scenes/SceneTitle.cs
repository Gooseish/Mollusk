using System;
using Microsoft.Xna.Framework;
using MolluskEngine.Scene;

namespace MolluskEngine.Scenes;

public class SceneTitle : _Scene
{
    private SceneMenu menu = new();
    public override void Update(GameTime gameTime)
    {
        menu.Update(gameTime);
    }
}
