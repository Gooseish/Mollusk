using System;
using MolluskEngine.Input;

namespace MolluskEngine.Menus;

public abstract class Node
{
    public Func<CommandResult>? Callback;
}
