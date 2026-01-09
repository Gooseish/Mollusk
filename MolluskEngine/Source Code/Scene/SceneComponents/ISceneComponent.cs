using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MolluskEngine.Scene;

public interface ISceneComponent
{
    public void Update(GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch);
}
