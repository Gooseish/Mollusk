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
        RefreshFields((int)newValue);
    }
    private void RefreshFields(int? index)
    {
        if (index == null
                || index < 0
                || index >= TerrainData.Count)
            return;
        Id = TerrainData[(int)index].Id;
        Name = TerrainData[(int)index].Name;
        Avoid = TerrainData[(int)index].Avoid;
        Def = TerrainData[(int)index].Def;
        Res = TerrainData[(int)index].Res;
        HealPercent = TerrainData[(int)index].HealPercent;
    }
    
    [ObservableProperty]
    private ObservableCollection<TerrainDataViewModel> _terrainData;

    public TerrainEditorViewModel()
    {
        Initialize();
        SaveLoadService.ProjectLoaded += OnProjectLoaded;
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
            RefreshFields(SelectedTerrain);
        }
    }
    private void OnProjectLoaded(object? sender, EventArgs args)
    {
        Initialize();
    }
    public void Unsubscribe()
    {
        SaveLoadService.ProjectLoaded -= OnProjectLoaded;
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
        if (newValue == null)
            return;
        TerrainData[(int)SelectedTerrain].Name = (string)newValue;
    }
    [ObservableProperty]
    private int? _avoid;
    partial void OnAvoidChanged(int? oldValue, int? newValue)
    {
        if (newValue == null)
            return;
        TerrainData[(int)SelectedTerrain].Avoid = (int)newValue;
    }

    [ObservableProperty]
    private int? _def;
    partial void OnDefChanged(int? oldValue, int? newValue)
    {
        if (newValue == null)
            return;
        TerrainData[(int)SelectedTerrain].Def = (int)newValue;
    }
    [ObservableProperty]
    private int? _res;
    partial void OnResChanged(int? oldValue, int? newValue)
    {
        if (newValue == null)
            return;
        TerrainData[(int)SelectedTerrain].Res = (int)newValue;
    }

    [ObservableProperty]
    private int? _healPercent;
    partial void OnHealPercentChanged(int? oldValue, int? newValue)
    {
        if (newValue == null)
            return;
        TerrainData[(int)SelectedTerrain].HealPercent = (int)newValue;
    }
    #endregion
}