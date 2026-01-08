using System;
using Microsoft.Xna.Framework;
using MolluskEngine.Input;
using MolluskEngine.Scene;
using MolluskEngine.Scenes;
using MolluskEngine.Settings;

namespace MolluskEngine;

public static class Global
{
    public static bool ExitCalling = false;
    public static InputMapper Input {get; private set;}
    public static _Scene Scene {get; private set;}
    public static _Settings Settings {get; private set;}
    public static void Update(GameTime gameTime)
    {
        Input.Update(gameTime);
        Scene.Update(gameTime);
    }
    public static void Initialize()
    {
        Settings = new _Settings();
        Input = new InputMapper();
        Scene = new SceneTitle();
    }
    public static void LoadContent()
    {
        
    }
}
