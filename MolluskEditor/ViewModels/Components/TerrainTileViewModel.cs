using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Extensions;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class TerrainTileViewModel : ViewModelBase
{
    private static DataModel<Terrain> _terrainData;
    private static Color _defaultColor = Colors.Transparent;
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
        if (Id == null) return _defaultColor;
        if (!_terrainData.Data.ContainsKey((int)Id)) return _defaultColor;
        return _terrainData.Data[(int)Id].TileColor.ToAvaloniaColor();
    }
    public void RefreshColor()
    {
        Brush.Color = GetColor();
    }
    public void AssignTerrain(string? idString)
    {
        if (!int.TryParse(idString, out int id))
            return;
        Id = id;
    }
}


