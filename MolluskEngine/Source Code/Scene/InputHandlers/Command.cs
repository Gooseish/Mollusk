using System;

namespace MolluskEngine.Scene;

public class Command
{
    public Func<CommandResult> Callback;
    public Type RequestedSignalType;
}
