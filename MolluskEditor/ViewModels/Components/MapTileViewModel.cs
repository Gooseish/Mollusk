using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Extensions;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class MapTileViewModel : ViewModelBase
{
    [ObservableProperty]
    private int? _id;
    public MapTileViewModel(int id, int index)
    {
        
    }
}
