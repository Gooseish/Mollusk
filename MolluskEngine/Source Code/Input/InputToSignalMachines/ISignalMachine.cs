using System;

namespace MolluskEngine.Input;

public interface ISignalMachine
{
    /// <summary>
    /// Defines whether the signal is being broadcast.
    /// </summary>
    public bool isActive {get;set;}
    /// <summary>
    /// Start broadcasting the signal.
    /// </summary>
    public void Update(KeyState keyState);
    /// <summary>
    /// If the signal was received, stop broadcasting the signal 
    /// and update the state depending on what was done with
    /// the signal.
    /// </summary>
    /// <param name="result"></param>
    public void Feedback(CommandResult result);
}
