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
    private int? _tilesetId;
    [ObservableProperty]
    private int? _tileId;
    public int Index;
    public (int TilesetId, int TileId)? Id 
    {
        get
        {
            if (TilesetId == null || TileId == null)
                return null;
            return ((int)TilesetId, (int)TileId);
        }
    }
    public MapTileViewModel(int tilesetId, int tileId, int index)
    {
        _tilesetId = tilesetId;
        _tileId = tileId;
        Index = index;
    }
    public void AssignTile((int TilesetId, int TileId) id)
    {
        TilesetId = id.TilesetId;   
        TileId = id.TileId;
    }
}
