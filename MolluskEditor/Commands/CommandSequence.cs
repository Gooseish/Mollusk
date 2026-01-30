using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MolluskEditor.Commands;

/// <summary>
/// Object that encapsulates a sequence of consecutive commands
/// </summary>
public class CommandSequence : Command
{
    private List<Command> _commands = [];
    /// <summary>
    /// When Do() is first called, the command queue is
    /// calcified, preventing further modifications to the
    /// list of commands.
    /// </summary>
    private bool _calcified;
    public void Add(Command command)
    {
        if (_calcified)
            throw new Exception("Tried to add commands to an already executed command sequence");
        _commands.Add(command);
    }

    public override void Do()
    {
        _calcified = true;
        foreach(Command command in _commands)
        {
            command.Do();
        }
    }

    public override void Undo()
    {
        for (int n = _commands.Count - 1; n >= 0; n--)
            _commands[n].Undo();
    }
}
