using System;
using System.Collections.Generic;
using MolluskEngine.Input;

namespace MolluskEngine.Scene;

public abstract class SceneInputHandler
{
    public Dictionary<CommandName, Command> CommandRouter;
    public void Update()
    {   
        foreach (CommandName commandName in CommandRouter.Keys)
        {
            CommandRouter[commandName].Update(commandName);
        }
    }
}

public enum CommandResult
{
    Null, Accepted, Rejected
}