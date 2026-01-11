using System;
using MolluskEngine.Scene;

namespace MolluskEngine.Menus;

public abstract class Node
{
    public Func<CommandResult>? Callback;
}
