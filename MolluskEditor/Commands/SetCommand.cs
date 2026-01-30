using System;

namespace MolluskEditor.Commands;

public class SetCommand<T> : Command
{
    private Action<T> _setValue;
    private readonly T _oldValue;
    private readonly T _newValue;
    public SetCommand(Action<T> setValue, T? oldValue, T newValue)
    {
       _setValue = setValue;
       _oldValue = oldValue;
       _newValue = newValue;
    }
    public override void Do()
    {
        _setValue(_newValue);
    }

    public override void Undo()
    {
        _setValue(_oldValue);
    }
}
