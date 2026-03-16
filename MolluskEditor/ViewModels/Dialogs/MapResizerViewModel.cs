using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class MapResizerViewModel : DialogViewModel
{
    private MapDataViewModel _map;
    public MapResizerViewModel(MapDataViewModel map)
    {
        _map = map;
        _width = map.Width;
        _height = map.Height;
        XOffset = "0";
        YOffset = "0";
    }
    [RelayCommand]
    private void Confirm()
    {
        Close();
    }
    [RelayCommand]
    private void Cancel()
    {
        Close();
    }
    [ObservableProperty]
    private string _width;
    [ObservableProperty]
    private string _height;
    [ObservableProperty]
    private string _xOffset;
    [ObservableProperty]
    private string _yOffset;
}
