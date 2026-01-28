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
    private Stack<Command> UndoStack = [];
    private Stack<Command> RedoStack = [];
    public void IssueCommand(Command command)
    {
        command.Do();
        UndoStack.Push(command);
        RedoStack.Clear();
    }
    public void Undo()
    {
        if (UndoStack.Count == 0)
            return;
        Command command = UndoStack.Pop();
        command.Undo();
        RedoStack.Push(command);
        
        if (OnUndo != null)
            OnUndo.Invoke(null, EventArgs.Empty);
    }
    public void Redo()
    {
        if (RedoStack.Count == 0)
            return;
        Command command = RedoStack.Pop();
        command.Do();
        UndoStack.Push(command);

        if (OnRedo != null)
            OnRedo.Invoke(null, EventArgs.Empty);
    }
    private void Clear(object? sender, EventArgs args)
    {
        // Could subscribe to the OnLoad event if this was a singleton
        UndoStack.Clear();
        RedoStack.Clear();
    }
    public EventHandler? OnUndo;
    public EventHandler? OnRedo;
}
