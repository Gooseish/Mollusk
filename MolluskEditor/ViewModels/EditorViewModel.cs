using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Data;

namespace MolluskEditor.ViewModels;

public partial class EditorViewModel : ViewModelBase
{
    [ObservableProperty]
    private EditorName _editorName;
}
