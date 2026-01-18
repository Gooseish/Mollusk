using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Data;
using MolluskEditor.Factories;

namespace MolluskEditor.ViewModels;

public partial class ChildWindowViewModel: ViewModelBase
{
    [ObservableProperty]
    private EditorViewModel? _currentEditor;
    public string CurrentEditorName{get{return CurrentEditor.EditorName.ToString();}} // Debug
    private EditorFactory _editorFactory;

    public ChildWindowViewModel(EditorFactory editorFactory)
    {
        _editorFactory = editorFactory;
    }
    public void GoToEditor(EditorName name)
    {
        CurrentEditor = _editorFactory.GetEditorViewModel(name);
    }
}
