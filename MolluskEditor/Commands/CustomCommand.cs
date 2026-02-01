using System;

namespace MolluskEditor.Commands;

public class CustomCommand : Command
{
    private Action _do;
    private Action _undo;
    public CustomCommand(Action __do, Action __undo)
    {
        _do = __do;
        _undo = __undo;
    }
    public override void Do()
    {
        _do.Invoke();
    }

    public override void Undo()
    {
        _undo.Invoke();
    }
}
