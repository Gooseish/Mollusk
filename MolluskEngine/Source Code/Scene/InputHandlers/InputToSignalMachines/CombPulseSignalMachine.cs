using System;
using MolluskEngine.Input;

namespace MolluskEngine.Scene;

public class CombPulseSignalMachine : ISignalMachine
{
    public bool isActive {get;set;}
    public int timer = 0;
    public enum SignalState
    {
        Idle,
        AfterInitialPulse,
        InPulseComb
    }
    public SignalState signalState = SignalState.Idle;
    public void Feedback(CommandResult result)
    {
        // Consume the command
        isActive = false;
        timer = 0;

        // State is idle if command did not go through
        if (result != CommandResult.Accepted)
        {
            signalState = SignalState.Idle;
            return;
        }
        // Otherwise, evolve the state
        switch (signalState)
        {
            case SignalState.Idle:
                signalState = SignalState.AfterInitialPulse;
                return;
            case SignalState.AfterInitialPulse:
                signalState = SignalState.InPulseComb;
                return;
            case SignalState.InPulseComb:
                return;
        }
    }

    public void SendSignal()
    {
        isActive = true;
    }

    public void Update(KeyState keyState)
    {
        // Reset signal state
        isActive = false;
        // Idle if key is not held
        if (!keyState.KeyDown)
        {
            signalState = SignalState.Idle;
            return;
        }
        // Fire signal under appropriate conditions
        switch (signalState)
        {
            case SignalState.Idle:
                if (keyState.KeyPressed)
                    SendSignal();
                break;
            case SignalState.AfterInitialPulse:
                if (timer == 30)
                    SendSignal();
                break;
            case SignalState.InPulseComb:
                if (timer == 5)
                    SendSignal();
                break;
        }
        // Increment timer
        timer++;
    }
}
