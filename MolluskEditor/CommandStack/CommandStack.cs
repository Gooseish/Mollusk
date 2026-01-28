using System;
using System.Collections.Generic;

namespace MolluskEditor.CommandStack;

public static class CommandStack // Should this be a static class or a singleton?
{
    private static Stack<Command> UndoStack;

    private static Stack<Command> RedoStack;
    public static void IssueCommand(Command command)
    {
        command.Do();
        UndoStack.Push(command);
        RedoStack.Clear();
    }
    public static void Undo()
    {
        if (UndoStack.Count == 0)
            return;
        Command command = UndoStack.Pop();
        command.Undo();
        RedoStack.Push(command);
    }
    public static void Redo()
    {
        if (RedoStack.Count == 0)
            return;
        Command command = RedoStack.Pop();
        command.Do();
        UndoStack.Push(command);
    }
}
