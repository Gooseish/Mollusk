using System;
using MolluskEditor.Data;
using MolluskEditor.Views;

namespace MolluskEditor.Factories;

public class WindowFactory
{
    private readonly Func<EditorName, ChildWindowView> windowFactory;

    public WindowFactory(Func<EditorName, ChildWindowView> factory)
    {
        windowFactory = factory;
    }

    public ChildWindowView LaunchNewChildWindow(EditorName name)
    {
        var window = windowFactory.Invoke(name);
        window.Show();
        return window;
    }
}