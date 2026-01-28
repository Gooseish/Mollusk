using System;

namespace MolluskEditor.CommandStack;

public class CommandSet<T> : Command
{
    private T _target;
    private readonly T? _oldValue;
    private readonly T _newValue;
    public CommandSet(T target, T? oldValue, T newValue)
    {
       _target = target;
       _oldValue = oldValue;
       _newValue = newValue; 
    }
    public override void Do()
    {
        _target = _newValue;
    }

    public override void Undo()
    {
        _target = _oldValue;
    }
}
