using System;
using System.Diagnostics;
using MolluskEngine.Input;

namespace MolluskEngine.Scene;

public class Command
{
    public Func<CommandResult> Callback;
    //public Type RequestedSignalType;
    public ISignalMachine SignalMachine;

    public Command(Func<CommandResult> callback, Type requestedSignalType)
    {
        Callback = callback;
        SignalMachine = (ISignalMachine)Activator.CreateInstance(requestedSignalType);
    }
    public void Update(CommandName commandName)
    {
        KeyState keyState = Global.Input.GetKeyState(commandName);
        SignalMachine.Update(keyState);
        if (SignalMachine.isActive)
        {
            CommandResult commandResult = Callback.Invoke();
            SignalMachine.Feedback(commandResult);
        }
    }
}
