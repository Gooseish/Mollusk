using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MolluskEngine.Input;

namespace MolluskEngine.Settings;

public class InputSettings
{
    private Dictionary<CommandName, Keys> keyboardMap;
    public IReadOnlyDictionary<CommandName, Keys> KeyboardMap {get {return keyboardMap;}}

    public void RestoreDefaultSettings() // Todo: this should be a json rather than hardcoded
    {
        keyboardMap = new Dictionary<CommandName, Keys>()
        {
            {CommandName.Confirm, Keys.Z},
            {CommandName.Cancel, Keys.X},
            {CommandName.Up, Keys.Up},
            {CommandName.Down, Keys.Down},
            {CommandName.Left, Keys.Left},
            {CommandName.Right, Keys.Right},
            {CommandName.Start, Keys.Enter},
            {CommandName.Select, Keys.Back},
            {CommandName.Tab, Keys.A},
            {CommandName.Info, Keys.C},
            {CommandName.Escape, Keys.Escape},
        };
    }
    public void RestoreSavedSettings()
    {
        
    }
}
