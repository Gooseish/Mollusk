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
    public bool MenuActive {get {return Menus.Count > 0;}}
    public bool InspectActive;
    public int? CurrentMenuIndex; 
    public Menu? CurrentMenu {get {return CurrentMenuIndex != null ? Menus[(int)CurrentMenuIndex] : null;}}
    public SceneMenuInputHandler InputHandler;
    public SceneMenu()
    {
        InputHandler = new SceneMenuInputHandler(this);
    }

    public void AddMenu<MenuType>(params object[] paramArray) 
    {
        Menus.Add((Menu)Activator.CreateInstance(typeof(MenuType), args:paramArray));
        CurrentMenuIndex = 0;
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
        if (CurrentMenu.CurrentNode == null) 
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
        switch(command)
        {
            case CommandName.Up:
            case CommandName.Left:
                DecrementActiveNodeIndex();
                return CommandResult.Accepted;
            case CommandName.Down:
            case CommandName.Right:
                IncrementActiveNodeIndex();
            return CommandResult.Accepted;
        }
          
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
    private void IncrementActiveNodeIndex()
    {
        // CurrentMenu.activeNodeIndex = (activeMenu.activeNodeIndex + 1) % activeMenu.nodes.Count;
        CurrentMenu.CurrentNodeIndex = (CurrentMenu.CurrentNodeIndex + 1) % CurrentMenu.NodeCount;
    }
    private void DecrementActiveNodeIndex()
    {
        CurrentMenu.CurrentNodeIndex -= 1;
        // Loop back around
        if (CurrentMenu.CurrentNodeIndex < 0)
            CurrentMenu.CurrentNodeIndex += CurrentMenu.NodeCount;
    }
}
