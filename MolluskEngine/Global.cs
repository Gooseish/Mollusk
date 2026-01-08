using System;
using Microsoft.Xna.Framework;
using MolluskEngine.Input;
using MolluskEngine.Scene;
using MolluskEngine.Settings;

namespace MolluskEngine;

public static class Global
{
    public static bool ExitCalling = false;
    public static InputMapper Input;
    public static _Scene Scene;
    public static _Settings Settings;
    public static void Update(GameTime gameTime)
    {
        Input.Update(gameTime);
        Scene.Update(gameTime);
    }
    public static void Initialize()
    {
        Settings = new _Settings();
        Input = new InputMapper(Settings.InputSettings);
        Scene = new _Scene();
    }
    public static void LoadContent()
    {
        
    }
}
