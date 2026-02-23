using System;
using System.Collections.Generic;
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
    private static Dictionary<int, Bitmap> _images;
    public static void InjectDependency(Dictionary<int, Bitmap> images)
    {
        _images = images;
    }
    [ObservableProperty]
    private int? _tilesetId;
    [ObservableProperty]
    private int? _tileId;
    [ObservableProperty]
    private CroppedBitmap _brush;
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
        RefreshBrush();
    }
    public PixelRect GetSourceRect(int tilesetWidth)
    {
        int X = (int)_tileId % tilesetWidth * Config.tileWidth;
        int Y = (int)_tileId / tilesetWidth * Config.tileHeight;
        return new PixelRect(X, Y, Config.tileWidth, Config.tileHeight);
    }
    public void RefreshBrush()
    {
        Bitmap image = _images[TilesetId.Value];
        int tilesetWidth = image.PixelSize.Width / Config.tileWidth;
        PixelRect sourceRect = GetSourceRect(tilesetWidth);
        CroppedBitmap croppedImage = new CroppedBitmap(image, sourceRect);
        Brush = croppedImage;
    }
}
