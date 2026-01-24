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
    private int? _selectedTerrainIndex;
    [ObservableProperty]
    private ObservableCollection<TerrainDataViewModel> _terrainData;
    [ObservableProperty]
    private TerrainDataViewModel? _selectedTerrain;
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
            SelectedTerrainIndex = 0;
        }
    }
    [RelayCommand]
    private void AddTerrainData()
    {
        TerrainData.Add(new TerrainDataViewModel());
        SelectedTerrainIndex = TerrainData.Count - 1;
    }
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