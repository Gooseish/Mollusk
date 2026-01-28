using System;
using System.Collections.Generic;
using MolluskEditor.Services;

namespace MolluskEditor.Commands;

public class CommandStack // Should this be a static class or a singleton?
{
    public CommandStack()
    {
        SaveLoadService.ProjectLoaded += Clear;
    }
    private Stack<Command> _undoStack = [];
    private Stack<Command> _redoStack = [];
    public void IssueCommand(Command command)
    {
        command.Do();
        _undoStack.Push(command);
        _redoStack.Clear();
    }
    public void Undo()
    {
        if (_undoStack.Count == 0)
            return;
        Command command = _undoStack.Pop();
        command.Undo();
        _redoStack.Push(command);
        
        if (OnUndo != null)
            OnUndo.Invoke(null, EventArgs.Empty);
    }
    public void Redo()
    {
        if (_redoStack.Count == 0)
            return;
        Command command = _redoStack.Pop();
        command.Do();
        _undoStack.Push(command);

        if (OnRedo != null)
            OnRedo.Invoke(null, EventArgs.Empty);
    }
    private void Clear(object? sender, EventArgs args)
    {
        _undoStack.Clear();
        _redoStack.Clear();
    }
    public EventHandler? OnUndo;
    public EventHandler? OnRedo;
}
