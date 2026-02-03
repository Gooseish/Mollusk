using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MolluskEditor.ViewModels;

public partial class TerrainTileViewModel : ViewModelBase
{
    [ObservableProperty]
    private int? _id;
    partial void OnIdChanged(int? oldValue, int? newValue)
    {
        ReferenceImage();
    }
    [ObservableProperty]
    private Bitmap? _image;
    public TerrainTileViewModel(int id)
    {
        _id = id;
        ReferenceImage();
    }
    private void ReferenceImage()
    {
        if (Id == null) { Image = null; return; }
        try { Image = TilesetDataViewModel.TerrainTileImages[(int)Id]; }
        catch { Image = null; return; }
    }
    /*
    private Color GetColor()
    {
        if (Id == null) return Microsoft.Xna.Framework.Color.Transparent.ToAvaloniaColor(); // lol
        return _terrainData.Data[(int)Id].TileColor.ToAvaloniaColor();
    } 
    
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


