using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Extensions;
using MolluskEditor.Models;
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
    public int Index;
    partial void OnIdChanged(int? oldValue, int? newValue)
    {
        Brush.Color = GetColor();
    }
    //[ObservableProperty]
    //private Bitmap? _image;
    [ObservableProperty]
    private SolidColorBrush _brush = new SolidColorBrush();
    public TerrainTileViewModel(int id, int index)
    {
        _id = id;
        Index = index;
        Brush.Color = GetColor();
    }
    
    private Color GetColor()
    {
        if (Id == null) return Microsoft.Xna.Framework.Color.Transparent.ToAvaloniaColor(); // lol
        return _terrainData.Data[(int)Id].TileColor.ToAvaloniaColor();
    } 
    /*
    private void RegenerateImage()
    {
        Image = EditorExtensions.BitmapFromColor(
            1, 1, GetColor());
    }
    */
    [RelayCommand]
    private void AssignTerrain(string? idString)
    {
        if (!int.TryParse(idString, out int id))
            return;
        Id = id;
    }
}


