using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Commands;

namespace MolluskEditor.ViewModels;

public partial class MapsEditorViewModel : EditorViewModel
{
    [ObservableProperty]
    private DrawingImage _myDrawingImage;
    public MapsEditorViewModel(CommandStack commandStack) : base(commandStack)
    {
        _myDrawingImage = new();
    }

    public override void Dispose()
    {
        
    }
}
