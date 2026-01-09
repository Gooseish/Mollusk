using System;
using System.Collections.Generic;
using MolluskEngine.Input;

namespace MolluskEngine.Scene;

public class SceneTitleInputHandler : SceneInputHandler
{
    public SceneTitleInputHandler(SceneTitle sceneTitle)
    {
        CommandRouter = new Dictionary<CommandName, Command>();
        CommandRouter[CommandName.Start] = new Command(sceneTitle.OpenMenu, typeof(CombPulseSignalMachine));
    }
}
