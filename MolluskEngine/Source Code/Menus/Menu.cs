using System;
using System.Collections.Generic;

namespace MolluskEngine.Menus;

public abstract class Menu
{
    public List<Node> Nodes;
    public Node? CurrentNode;
}
