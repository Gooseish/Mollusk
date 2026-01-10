using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MolluskEngine.Input;
using MolluskEngine.Menus;
using MolluskEngine.Source_Code.Scene.InputHandlers;

namespace MolluskEngine.Scene;

public class SceneMenu : ISceneComponent
{
    public List<Menu> Menus = new();
    public bool MenuActive {get {return Menus.Count > 1;}}
    public bool InspectActive;
    public int? CurrentMenuIndex; 
    public Menu? CurrentMenu {get {return CurrentMenuIndex != null ? Menus[(int)CurrentMenuIndex] : null;}}
    public SceneMenuInputHandler InputHandler;
    public SceneMenu()
    {
        InputHandler = new SceneMenuInputHandler(this);
    }

    public void AddMenu<MenuType>() 
    {
        Menus.Add((Menu)Activator.CreateInstance(typeof(MenuType)));
        CurrentMenuIndex = 0;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        
    }

    public void Update(GameTime gameTime)
    {
        
    }
    // Cancel
    public CommandResult TryCancel()
    {
        CommandResult result = CommandResult.Null;
        try
        {
            CallMenuClose();
            result = CommandResult.Accepted;
        }
        finally { }
        return result;
    }
    public void CallMenuClose()
    {
        Menus.RemoveAt(Menus.Count - 1);
        if (Menus.Count == 0)
            CurrentMenuIndex = null;
    }
    // Confirm
    public CommandResult CallCurrentNode()
    {
        if (CurrentMenu.Nodes.Count == 0) // Menu has nodes
            return CommandResult.Null;
        if (CurrentMenu.CurrentNode.Callback == null)
            return CommandResult.Null;
        return CurrentMenu.CurrentNode.Callback.Invoke();
    }
    // Info
    public CommandResult TryInspect()
    {
        /*
        if (State.activeMenu is IMenuNodeMap activeMenu)
            if (activeMenu.inspectable)
            {
                activeMenu.inspectActive = !activeMenu.inspectActive;
                return CommandResult.Accepted;
            }
        */
        return CommandResult.Null;
    }
    // Tab
    public CommandResult TryTab()
    {
        return CommandResult.Null;
    }
    // Directional Input
    public CommandResult TryUp() {return TryDirectionalInput(CommandName.Up);}
    public CommandResult TryDown() {return TryDirectionalInput(CommandName.Down);}
    public CommandResult TryLeft() {return TryDirectionalInput(CommandName.Left);}
    public CommandResult TryRight() {return TryDirectionalInput(CommandName.Right);}
    public CommandResult TryDirectionalInput(CommandName command)
    {
        /*
        if (State.activeMenu is IMenuNodeMap activeMenu)
            switch (activeMenu.nodeMapType)
            {
                case NodeMapType.Linear:
                    switch (command)
                    {
                        case CommandName.Up:
                        case CommandName.Left:
                            DecrementActiveNodeIndex(activeMenu);
                            return CommandResult.Accepted;
                        case CommandName.Down:
                        case CommandName.Right:
                            IncrementActiveNodeIndex(activeMenu);
                            return CommandResult.Accepted;
                    }
                    break;
                default:
                    break;
            }
        */
        return CommandResult.Null;
    }
    /*
    private static void IncrementActiveNodeIndex(IMenuNodeMap activeMenu)
    {
        activeMenu.activeNodeIndex = (activeMenu.activeNodeIndex + 1) % activeMenu.nodes.Count;
    }
    private static void DecrementActiveNodeIndex(IMenuNodeMap activeMenu)
    {
        activeMenu.activeNodeIndex -= 1;
        // Loop back around
        if (activeMenu.activeNodeIndex < 0)
            activeMenu.activeNodeIndex += activeMenu.nodes.Count;
    }
    */
}
