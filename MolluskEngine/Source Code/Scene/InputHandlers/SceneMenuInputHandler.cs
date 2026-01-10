using System;
using System.Collections.Generic;
using MolluskEngine.Input;
using MolluskEngine.Scene;

namespace MolluskEngine.Source_Code.Scene.InputHandlers;

public class SceneMenuInputHandler : SceneInputHandler
{
    public SceneMenuInputHandler(SceneMenu sceneMenu)
    {
        CommandRouter = new Dictionary<CommandName, Command>();
        CommandRouter[CommandName.Confirm] = new Command(sceneMenu.CallCurrentNode, typeof(CombPulseSignalMachine));
        CommandRouter[CommandName.Cancel] = new Command(sceneMenu.TryCancel, typeof(CombPulseSignalMachine));
        CommandRouter[CommandName.Up] = new Command(sceneMenu.TryUp, typeof(CombPulseSignalMachine));
        CommandRouter[CommandName.Down] = new Command(sceneMenu.TryDown, typeof(CombPulseSignalMachine));
        CommandRouter[CommandName.Left] = new Command(sceneMenu.TryLeft, typeof(CombPulseSignalMachine));
        CommandRouter[CommandName.Right] = new Command(sceneMenu.TryRight, typeof(CombPulseSignalMachine));
    }
}
