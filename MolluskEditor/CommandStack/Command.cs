using System;

namespace MolluskEditor.CommandStack;

public abstract class Command
{
    public abstract void Do();
    public abstract void Undo();
}
