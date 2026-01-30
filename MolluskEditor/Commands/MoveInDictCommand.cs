using System;

namespace MolluskEditor.Commands;

public class MoveInDictCommand : Command
{
    private Action<int, int> _moveItem;
    private readonly int _oldIndex;
    private readonly int _newIndex;
    public MoveInDictCommand(Action<int, int> moveItem, int oldValue, int newValue)
    {
       _moveItem = moveItem;
       _oldIndex = oldValue;
       _newIndex = newValue; 
    }
    public override void Do()
    {
        _moveItem(_newIndex, _oldIndex);
    }

    public override void Undo()
    {
        _moveItem(_oldIndex, _newIndex);
    }
}
