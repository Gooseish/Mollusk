using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MolluskEngine.Menus;

public abstract class Menu
{
    public List<Node> Nodes;
    public Node? CurrentNode;
    public abstract void Draw(SpriteBatch spriteBatch);
}
