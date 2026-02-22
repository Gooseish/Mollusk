using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Extensions;
using MolluskEditor.Models;
using MolluskEngine;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class MapTileViewModel : ViewModelBase
{
    [ObservableProperty]
    private int? _tilesetId;
    [ObservableProperty]
    private int? _tileId;
    [ObservableProperty]
    private ImageBrush _brush;
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
    public RelativeRect GetSourceRect(int tilesetWidth)
    {
        int X = (int)_tileId % tilesetWidth;
        int Y = (int)_tileId / tilesetWidth;
        Rect result = new Rect(X, Y, Config.tileWidth, Config.tileHeight);
        return new RelativeRect(result, RelativeUnit.Absolute);
    }
    public void RefreshBrush(Bitmap image, int tilesetWidth)
    {
        Brush = new ImageBrush(image)
            { SourceRect = GetSourceRect(tilesetWidth) };
    }
}
