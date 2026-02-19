using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Commands;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class MapsEditorViewModel : EditorViewModel
{
    private DataModel<GameMap> _dataModel;
    private DataModel<Tileset> _tilesetDataModel;
    [ObservableProperty]
    private DataSelectorViewModel _data;
    [ObservableProperty]
    private MapDataViewModel? _selectedMap;
    [ObservableProperty]
    private DataSelectorViewModel _tilesetData;
    [ObservableProperty]
    private TilesetDataViewModel? _selectedTileset;
    [ObservableProperty]
    private int _selectedTile;
    public MapsEditorViewModel(CommandStack commandStack) : base(commandStack)
    {
        
    }

    public override void Dispose()
    {
        
    }
}
