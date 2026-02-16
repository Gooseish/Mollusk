using System;

namespace MolluskEditor.Commands;
// Danger! Set command does not function as intended with reference values! Copies must be made!
public class SetInCollectionCommand<T> : Command
{
    private Action<int, T> _setValue;
    private readonly T _oldValue;
    private readonly T _newValue;
    private readonly int _index;
    public SetInCollectionCommand(Action<int, T> setValue, int index, T? oldValue, T newValue)
    {
       _setValue = setValue;
       _index = index;
       _oldValue = oldValue;
       _newValue = newValue;
    }
    public override void Do()
    {
        _setValue(_index, _newValue);
    }

    public override void Undo()
    {
        _setValue(_index, _oldValue);
    }
}