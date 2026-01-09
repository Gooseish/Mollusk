using System;
using System.Collections.Generic;
using MolluskEngine.Input;

namespace MolluskEngine.Scene;

public class SceneTitleInputHandler : ISceneInputHandler
{
    public Dictionary<CommandName, Command> CommandRouter;
    
}
