using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Extensions;
using MolluskEditor.Models;
using MolluskEngine;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class TerrainTileViewModel : ViewModelBase
{
    private static DataModel<Terrain> _terrainData;
    public static void InjectDependency(DataModel<Terrain> terrainData)
    {
        _terrainData = terrainData;
    }
    [ObservableProperty]
    private int? _id;
    partial void OnIdChanged(int? oldValue, int? newValue)
    {
        RegenerateImage();
    }
    [ObservableProperty]
    private Bitmap _image;
    public TerrainTileViewModel(int id)
    {
        _id = id;
        RegenerateImage();
    }
    private Color GetColor()
    {
        if (Id == null) return Microsoft.Xna.Framework.Color.Transparent.ToAvaloniaColor(); // lol
        return _terrainData.Data[(int)Id].TileColor.ToAvaloniaColor();
    } 
    private void RegenerateImage()
    {
        Image = EditorExtensions.BitmapFromColor(
            Config.tileWidth, Config.tileHeight, GetColor());
    }
    [RelayCommand]
    private void AssignTerrain(string? idString)
    {
        if (!int.TryParse(idString, out int id))
            return;
        Id = id;
    }
}


