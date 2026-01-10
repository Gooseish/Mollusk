using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MolluskEngine.Scene;

namespace MolluskEngine.Menus;

public class TitleMenu : Menu
{
    public TitleMenu(SceneMenu sceneMenu)
    {
        Nodes = new List<Node>()
        {
            new TitleMenuNode(new Vector2(100, 100), "New Game", null),
            new TitleMenuNode(new Vector2(100, 200), "Load Game", null),
            new TitleMenuNode(new Vector2(100, 300), "Settings", null),
            new TitleMenuNode(new Vector2(100, 400), "Quit", null),
        };
    }
}
