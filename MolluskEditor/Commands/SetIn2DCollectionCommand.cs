using System;

namespace MolluskEditor.Commands;
// Danger! Set command does not function as intended with reference values! Copies must be made!
public class SetIn2DCollectionCommand<T> : Command
{
    private Action<int, int, T> _setValue;
    private readonly T _oldValue;
    private readonly T _newValue;
    private readonly int _rowIndex;
    private readonly int _columnIndex;
    public SetIn2DCollectionCommand(Action<int, int, T> setValue, int rowIndex, int columnIndex,
        T? oldValue, T newValue)
    {
       _setValue = setValue;
       _rowIndex = rowIndex;
       _columnIndex = columnIndex;
       _oldValue = oldValue;
       _newValue = newValue;
    }
    public override void Do()
    {
        _setValue(_rowIndex, _columnIndex, _newValue);
    }

    public override void Undo()
    {
        _setValue(_rowIndex, _columnIndex, _oldValue);
    }
}