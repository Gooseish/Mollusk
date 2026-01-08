using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MolluskEngine.Scene;

public abstract class _Scene
{
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(GraphicsDevice graphicsDevice);
}
