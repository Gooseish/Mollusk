using System;
using System.Collections.Generic;
using MolluskEngine.Input;

namespace MolluskEngine.Scene;

public class SceneMenuInputHandler : SceneInputHandler
{
    public SceneMenuInputHandler(SceneMenu sceneMenu)
    {
        CommandRouter = new Dictionary<CommandName, Command>();
        CommandRouter[CommandName.Confirm] = new Command(sceneMenu.CallCurrentNode, typeof(SinglePulseMachine));
        CommandRouter[CommandName.Cancel] = new Command(sceneMenu.TryCancel, typeof(SinglePulseMachine));
        CommandRouter[CommandName.Up] = new Command(sceneMenu.TryUp, typeof(CombPulseSignalMachine));
        CommandRouter[CommandName.Down] = new Command(sceneMenu.TryDown, typeof(CombPulseSignalMachine));
        CommandRouter[CommandName.Left] = new Command(sceneMenu.TryLeft, typeof(CombPulseSignalMachine));
        CommandRouter[CommandName.Right] = new Command(sceneMenu.TryRight, typeof(CombPulseSignalMachine));
    }
}
