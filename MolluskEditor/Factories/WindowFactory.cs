using System;
using MolluskEditor.ViewModels;
using MolluskEditor.Views;

namespace MolluskEditor.Factories;

public class WindowFactory
{
    private readonly Func<EditorName, ChildWindowView> windowFactory;

    public WindowFactory(Func<EditorName, ChildWindowView> factory)
    {
        windowFactory = factory;
    }

    public ChildWindowViewModel LaunchNewChildWindow(EditorName name)
    {
        var window = windowFactory.Invoke(name);
        window.Show();
        return (ChildWindowViewModel)window.DataContext;
    }
}