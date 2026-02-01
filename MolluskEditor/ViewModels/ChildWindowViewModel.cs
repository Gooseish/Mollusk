using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Factories;

namespace MolluskEditor.ViewModels;

public partial class ChildWindowViewModel: ViewModelBase
{
    [ObservableProperty]
    private EditorViewModel? _currentEditor;
    public string CurrentEditorName{get{return CurrentEditor.EditorName.ToString();}} // Debug
    public EditorName? EditorName {get{return CurrentEditor?.EditorName;}} // Shouldn't be nullable?
    private EditorFactory _editorFactory;

    public ChildWindowViewModel(EditorFactory editorFactory)
    {
        _editorFactory = editorFactory;
    }
    public void GoToEditor(EditorName name)
    {
        CurrentEditor = _editorFactory.GetEditorViewModel(name);
    }
    public void OnClose(object sender, EventArgs eventArgs)
    {
        if (ChildWindowClosed == null)
            return;
        ChildWindowClosed.Invoke(this, EventArgs.Empty);
    }
    public EventHandler? ChildWindowClosed;
}
