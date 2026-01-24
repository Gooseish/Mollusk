using System;

namespace MolluskEngine.Input;

/// <summary>
/// Send the command signal once and only once when its associated
/// button is pressed.
/// </summary>
public class SinglePulseMachine : ISignalMachine
{
    public bool isActive {get;set;}

    public void Feedback(CommandResult result)
    {
        isActive = false;
    }

    public void SendSignal()
    {
        isActive = true;
    }

    public void Update(KeyState keyState)
    {
        if (keyState.KeyPressed)
            SendSignal();
    }
}