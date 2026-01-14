using System;
using Microsoft.Xna.Framework;

namespace MolluskEngine.Scene;

public interface ISceneComponent
{
    public void Update(GameTime gameTime);
}
