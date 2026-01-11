using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MolluskEngine.Menus;

public abstract class Menu
{
    public int? CurrentNodeIndex;
    public abstract int NodeCount {get;}
    public abstract Node? CurrentNode {get;}
    
    public abstract void Draw(SpriteBatch spriteBatch);
}
