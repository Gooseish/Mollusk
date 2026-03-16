using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MolluskEditor.ViewModels;

public abstract partial class DialogViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isDialogOpen;

    public void Show()
    {
        IsDialogOpen = true;
    }
    public void Close()
    {
        IsDialogOpen = false;
    }
}
