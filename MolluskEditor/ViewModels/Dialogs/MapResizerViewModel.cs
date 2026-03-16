using System;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Validators;

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
        if (GetErrors().Any()) return;
        _map.ResizeTilemap(
            int.Parse(Width), int.Parse(Height),
            int.Parse(XOffset), int.Parse(YOffset));
        Close();
    }
    [RelayCommand]
    private void Cancel()
    {
        Close();
    }
    [ObservableProperty]
    [NotifyDataErrorInfo][ParseAsInt]
    private string _width;
    [ObservableProperty]
    [NotifyDataErrorInfo][ParseAsInt]
    private string _height;
    [ObservableProperty]
    [NotifyDataErrorInfo][ParseAsInt]
    private string _xOffset;
    [ObservableProperty]
    [NotifyDataErrorInfo][ParseAsInt]
    private string _yOffset;
}
