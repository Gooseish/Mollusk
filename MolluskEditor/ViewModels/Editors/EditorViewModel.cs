using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Commands;
using MolluskEditor.Data;

namespace MolluskEditor.ViewModels;

public abstract partial class EditorViewModel : ViewModelBase
{
    protected CommandStack _commandStack;
    public EditorViewModel(CommandStack commandStack)
    {
        _commandStack = commandStack;
    }
    [ObservableProperty]
    private EditorName _editorName;
    public abstract void Dispose();
}
