using System;
using Microsoft.Xna.Framework.Input;

namespace MolluskEngine.Input;

/// <summary>
/// Class that represents the state of a generic key (e.g.
/// "Up", "Down", "Confirm", Etc.)
/// </summary>
public class KeyState
{
    /// <summary>
    /// New key press detected?
    /// </summary>
    public bool KeyPressed {get; private set;} = false;
    /// <summary>
    /// Is the key being help down now?
    /// </summary>
    public bool KeyDown {get; private set;} = false;
    /// <summary>
    /// How long has the key been held down?
    /// </summary>
    public int TimeSpanHeld {get; private set;} = 0;
    /// <summary>
    /// New key release detected?
    /// </summary>
    public bool KeyReleased {get; private set;} = false;
    /// <summary>
    /// Was the key pressed, held for a short 
    /// amount of time, and then released?
    /// </summary>
    public bool FastKeyReleased {get; private set;} = false;
    /// <summary>
    /// Defines "short amount of time" for fastKeyReleased.
    /// </summary>

    public void Update(KeyboardState CurrentKeyboardState, Keys key)
    {
        KeyDown = CurrentKeyboardState.IsKeyDown(key);
        KeyPressed = KeyDown && TimeSpanHeld == 0;
        KeyReleased = !KeyDown && TimeSpanHeld > 0;
        FastKeyReleased = KeyReleased && (TimeSpanHeld <= Config.FAST_KEY_PRESS_TIMEFRAME);

        if (KeyDown)
            TimeSpanHeld += 1;
        else
            TimeSpanHeld = 0;
    }
}

public enum CommandName
{
    Confirm, Cancel, 
    Up, Down, Left, Right,
    Start, Select,
    Tab, Info,
    Escape
}