using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class TerrainEditorViewModel : EditorViewModel
{
    [ObservableProperty]
    private int _selectedTerrain;
    partial void OnSelectedTerrainChanged(int oldValue, int newValue)
    {
        Id = TerrainData[newValue].Id;
        Name = TerrainData[newValue].Name;
        Avoid = TerrainData[newValue].Avoid;
        Def = TerrainData[newValue].Def;
        Res = TerrainData[newValue].Res;
        HealPercent = TerrainData[newValue].HealPercent;
    }
    
    [ObservableProperty]
    private ObservableCollection<TerrainDataViewModel> _terrainData;

    public TerrainEditorViewModel()
    {
        EditorName = Data.EditorName.Terrain; // Unnecessary?
        TerrainData = [];

        getTerrainData();
        void getTerrainData()
        {
            foreach (Terrain terrain in TerrainDataModel.TerrainData.Values)
                TerrainData.Add(new TerrainDataViewModel(terrain));
        }
    }

    [RelayCommand]
    private void AddTerrainData()
    {
        TerrainData.Add(new TerrainDataViewModel());
        SelectedTerrain = TerrainData.Count - 1;
    }
    #region Terrain Data Properties
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string? _name;
    partial void OnNameChanged(string? oldValue, string? newValue)
    {
        if (newValue == null)
            return;
        TerrainData[SelectedTerrain].Name = (string)newValue;
    }
    [ObservableProperty]
    private int? _avoid;
    partial void OnAvoidChanged(int? oldValue, int? newValue)
    {
        if (newValue == null)
            return;
        TerrainData[SelectedTerrain].Avoid = (int)newValue;
    }

    [ObservableProperty]
    private int? _def;
    partial void OnDefChanged(int? oldValue, int? newValue)
    {
        if (newValue == null)
            return;
        TerrainData[SelectedTerrain].Def = (int)newValue;
    }
    [ObservableProperty]
    private int? _res;
    partial void OnResChanged(int? oldValue, int? newValue)
    {
        if (newValue == null)
            return;
        TerrainData[SelectedTerrain].Res = (int)newValue;
    }

    [ObservableProperty]
    private int? _healPercent;
    partial void OnHealPercentChanged(int? oldValue, int? newValue)
    {
        if (newValue == null)
            return;
        TerrainData[SelectedTerrain].HealPercent = (int)newValue;
    }
    #endregion
}