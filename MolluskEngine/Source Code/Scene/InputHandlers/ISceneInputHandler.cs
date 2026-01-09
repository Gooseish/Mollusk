using System;
using MolluskEngine.Scene;

namespace MolluskEngine.Scene;

public interface ISceneInputHandler
{
    // Todo: Maybe make this an abstract class?
}

public enum CommandResult
{
    Null, Accepted, Rejected
}