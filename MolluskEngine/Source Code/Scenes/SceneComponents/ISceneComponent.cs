using System;
using Microsoft.Xna.Framework;

namespace MolluskEngine.Scenes;

public interface ISceneComponent
{
    public void Update(GameTime gameTime);
}
