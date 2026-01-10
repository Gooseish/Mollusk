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

    }
}
