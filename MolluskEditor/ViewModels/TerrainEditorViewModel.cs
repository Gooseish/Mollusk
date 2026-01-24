using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Models;
using MolluskEditor.Services;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class TerrainEditorViewModel : EditorViewModel
{
    [ObservableProperty]
    private int? _selectedTerrain;
    partial void OnSelectedTerrainChanged(int? oldValue, int? newValue)
    {
        RefreshFields(newValue);
    }
    private void RefreshFields(int? index)
    {
        if (!ValidTerrainIndex(index))
            return;
        Id = TerrainData[(int)index].Id;
        Name = TerrainData[(int)index].Name;
        Avoid = TerrainData[(int)index].Avoid;
        Def = TerrainData[(int)index].Def;
        Res = TerrainData[(int)index].Res;
        HealPercent = TerrainData[(int)index].HealPercent;
    }
    private bool ValidTerrainIndex(int? index)
    {
        // Maybe should use a try except block here instead
        if (index == null)
            return false;
        if (index < 0)
            return false;
        if (index >= TerrainData.Count)
            return false;
        return true;
    }
    
    [ObservableProperty]
    private ObservableCollection<TerrainDataViewModel> _terrainData;

    public TerrainEditorViewModel()
    {
        Initialize();
        Subscribe();
    }
    private void Initialize()
    {
        EditorName = Data.EditorName.Terrain; // Unnecessary?

        ObservableCollection<TerrainDataViewModel> terrainData = [];
        getTerrainData();
        void getTerrainData()
        {
            foreach (Terrain terrain in TerrainDataModel.TerrainData.Values)
                terrainData.Add(new TerrainDataViewModel(terrain));
        }
        TerrainData = terrainData;
        if (TerrainData.Count > 0)
        {
            SelectedTerrain = 0;
            //RefreshFields(SelectedTerrain);
        }
    }
    [RelayCommand]
    private void AddTerrainData()
    {
        TerrainData.Add(new TerrainDataViewModel());
        SelectedTerrain = TerrainData.Count - 1;
        //RefreshFields(SelectedTerrain);
    }
    #region Terrain Data Properties
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string? _name;
    partial void OnNameChanged(string? oldValue, string? newValue)
    {
        if (newValue == null || !ValidTerrainIndex(SelectedTerrain))
            return;
        TerrainData[(int)SelectedTerrain].Name = (string)newValue;
    }
    [ObservableProperty]
    private int? _avoid;
    partial void OnAvoidChanged(int? oldValue, int? newValue)
    {
        if (newValue == null || !ValidTerrainIndex(SelectedTerrain))
            return;
        TerrainData[(int)SelectedTerrain].Avoid = (int)newValue;
    }

    [ObservableProperty]
    private int? _def;
    partial void OnDefChanged(int? oldValue, int? newValue)
    {
        if (newValue == null || !ValidTerrainIndex(SelectedTerrain))
            return;
        TerrainData[(int)SelectedTerrain].Def = (int)newValue;
    }
    [ObservableProperty]
    private int? _res;
    partial void OnResChanged(int? oldValue, int? newValue)
    {
        if (newValue == null || !ValidTerrainIndex(SelectedTerrain))
            return;
        TerrainData[(int)SelectedTerrain].Res = (int)newValue;
    }

    [ObservableProperty]
    private int? _healPercent;
    partial void OnHealPercentChanged(int? oldValue, int? newValue)
    {
        if (newValue == null || !ValidTerrainIndex(SelectedTerrain))
            return;
        TerrainData[(int)SelectedTerrain].HealPercent = (int)newValue;
    }
    #endregion
    #region Event Handling
    private void OnProjectLoaded(object? sender, EventArgs args)
    {
        Initialize();
    }
    private void Subscribe()
    {
        SaveLoadService.ProjectLoaded += OnProjectLoaded;
    }
    private void Unsubscribe()
    {
        SaveLoadService.ProjectLoaded -= OnProjectLoaded;
    }
    public override void Dispose()
    {
        Unsubscribe();
    }
    #endregion
}