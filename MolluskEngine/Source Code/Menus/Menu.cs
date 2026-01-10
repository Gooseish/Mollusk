using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MolluskEngine.Menus;

public abstract class Menu
{
    public List<Node> Nodes;
    public int? CurrentNodeIndex;
    public Node? CurrentNode {get
        {
            if (CurrentNodeIndex == null)
                return null;
            return Nodes[(int)CurrentNodeIndex];
        }
    }
    public abstract void Draw(SpriteBatch spriteBatch);
}
