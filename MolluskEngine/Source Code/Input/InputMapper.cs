using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MolluskEngine.Settings;

namespace MolluskEngine.Input;

public class InputMapper
{
    private KeyState[] keyStates;
    private IReadOnlyDictionary<CommandName, Keys> keyboardMap;
    private KeyboardState currentKeyboardState;
    public KeyState GetKeyState(CommandName commandName)
    {
        return keyStates[(int)commandName];
    }
    public void Update(GameTime gameTime)
    {
        currentKeyboardState = Keyboard.GetState();

        foreach(CommandName commandName in Enum.GetValues<CommandName>())
            keyStates[(int)commandName].Update(currentKeyboardState, keyboardMap[commandName]);
    }
    public InputMapper(InputSettings inputSettings)
    {
        keyboardMap = inputSettings.KeyboardMap;

        keyStates = new KeyState[Enum.GetValues<CommandName>().Length];
        foreach(CommandName commandName in Enum.GetValues<CommandName>())
            keyStates[(int)commandName] = new KeyState();
    }
}
