using System;
using MolluskEditor.ViewModels;

namespace MolluskEditor.Factories;

public class EditorFactory
{
    private readonly Func<EditorName, EditorViewModel> editorFactory;
    public EditorFactory(Func<EditorName, EditorViewModel> factory)
    {
        editorFactory = factory;
    }
    public EditorViewModel GetEditorViewModel(EditorName editorName)
    {
        return editorFactory.Invoke(editorName);
    }
}