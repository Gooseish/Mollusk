using System;
using Microsoft.Xna.Framework.Input;

namespace MolluskEngine.Input;

/// <summary>
/// Class that represents the key state of a generic key (e.g.
/// "Up", "Down", "Confirm", Etc.)
/// </summary>
public class KeyboardEvent
{
    /// <summary>
    /// New key press detected?
    /// </summary>
    public bool keyPressed = false;
    /// <summary>
    /// Is the key being help down now?
    /// </summary>
    public bool keyDown = false;
    /// <summary>
    /// How long has the key been held down?
    /// </summary>
    public int timeSpanHeld = 0;
    /// <summary>
    /// New key release detected?
    /// </summary>
    public bool keyReleased = false;
    /// <summary>
    /// Was the key pressed, held for a short 
    /// amount of time, and then released?
    /// </summary>
    public bool fastKeyReleased = false;
    /// <summary>
    /// Defines "short amount of time" for fastKeyReleased.
    /// </summary>
    static readonly int FAST_KEY_PRESS_TIMEFRAME = 10; // Todo: move to config?

    public void Update(KeyboardState CurrentKeyboardState, Keys key)
    {
        keyDown = CurrentKeyboardState.IsKeyDown(key);
        keyPressed = keyDown && timeSpanHeld == 0;
        keyReleased = !keyDown && timeSpanHeld > 0;
        fastKeyReleased = keyReleased && (timeSpanHeld <= FAST_KEY_PRESS_TIMEFRAME);

        if (keyDown)
            timeSpanHeld += 1;
        else
            timeSpanHeld = 0;
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