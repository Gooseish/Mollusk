using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MolluskEngine.Input;

public class InputMapper
{
    // Fields
    private KeyState[] keyStates;
    private IReadOnlyDictionary<CommandName, Keys> keyboardMap;
    private KeyboardState currentKeyboardState;
    // Accessors
    public KeyState GetKeyState(CommandName commandName)
    {
        return keyStates[(int)commandName];
    }
    // Update Loop
    public void Update(GameTime gameTime)
    {
        currentKeyboardState = Keyboard.GetState();

        foreach(CommandName commandName in Enum.GetValues<CommandName>())
            keyStates[(int)commandName].Update(currentKeyboardState, keyboardMap[commandName]);
    }
    // Constructor
    public InputMapper()
    {
        keyboardMap = Global.Settings.InputSettings.KeyboardMap;

        keyStates = new KeyState[Enum.GetValues<CommandName>().Length];
        foreach(CommandName commandName in Enum.GetValues<CommandName>())
            keyStates[(int)commandName] = new KeyState();
    }
}
