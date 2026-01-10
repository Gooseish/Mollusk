using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MolluskEngine.Graphics;
using MolluskEngine.Scene;

namespace MolluskEngine.Menus;

public class TitleMenu : Menu
{
    private SceneMenu menu;
    public TitleMenu(SceneMenu sceneMenu)
    {
        menu = sceneMenu;
        Nodes = new List<Node>()
        {
            new TitleMenuNode(new Vector2(100, 100), "New Game", OpenNewGameMenu),
            new TitleMenuNode(new Vector2(100, 200), "Load Game", OpenLoadGameMenu),
            new TitleMenuNode(new Vector2(100, 300), "Settings", OpenSettingsMenu),
            new TitleMenuNode(new Vector2(100, 400), "Quit", QuitGame),
        };
        CurrentNodeIndex = 0;
    }
    public CommandResult OpenNewGameMenu()
    {
        menu.AddMenu<NewGameMenu>();
        return CommandResult.Accepted;
    }
    public CommandResult OpenLoadGameMenu()
    {
        menu.AddMenu<LoadGameMenu>();
        return CommandResult.Accepted;
    }
    public CommandResult OpenSettingsMenu()
    {
        menu.AddMenu<SettingsMenu>();
        return CommandResult.Accepted;
    }
    public CommandResult QuitGame()
    {
        Global.ExitCalling = true;
        return CommandResult.Accepted;
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        foreach(Node node in Nodes)
        {
            Color drawColor = Color.White;
            if (node == CurrentNode)
                drawColor = Color.Gray;
            TitleMenuNode nodeCast = (TitleMenuNode)node;
            spriteBatch.Draw(GraphicalContent.MenuTextures["WhiteSquare"], 
                new Rectangle((int)nodeCast.Position.X, (int)nodeCast.Position.Y, nodeCast.Width, nodeCast.Height), 
                drawColor);
        }
    }
}
