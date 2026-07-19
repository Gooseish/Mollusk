using System;
using MolluskEditor.ViewModels;
using MolluskEditor.Views;

namespace MolluskEditor.Factories;

public class EditorWindowFactory
{
    private readonly Func<EditorName, EditorWindowView> windowFactory;

    public EditorWindowFactory(Func<EditorName, EditorWindowView> factory)
    {
        windowFactory = factory;
    }

    public EditorWindowViewModel LaunchNewChildWindow(EditorName name)
    {
        var window = windowFactory.Invoke(name);
        window.Show();
        return (EditorWindowViewModel)window.DataContext;
    }
}