using System;
using System.Collections.Generic;
using MolluskEngine.Input;

namespace MolluskEngine.Scene;

public class SceneTitleInputHandler : ISceneInputHandler
{
    public Dictionary<CommandName, Command> CommandRouter;
    public SceneTitleInputHandler(SceneTitle sceneTitle)
    {
        CommandRouter = new Dictionary<CommandName, Command>();
        CommandRouter[CommandName.Start] = new Command(sceneTitle.OpenMenu, typeof(CombPulseSignalMachine));
    }

    public void Update()
    {   
        foreach (CommandName commandName in CommandRouter.Keys)
        {
            CommandRouter[commandName].Update(commandName);
        }
    }
}
