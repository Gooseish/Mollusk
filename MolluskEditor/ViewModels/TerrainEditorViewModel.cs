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
        Avoid = TerrainData[SelectedTerrain].Avoid;
    }
    
    [ObservableProperty]
    private ObservableCollection<TerrainDataViewModel> _terrainData;

    [ObservableProperty]
    private int? _avoid;
    partial void OnAvoidChanged(int? oldValue, int? newValue)
    {
        if (newValue == null)
            return;
        TerrainData[SelectedTerrain].Avoid = (int)newValue;
    }

    public TerrainEditorViewModel()
    {
        EditorName = Data.EditorName.Terrain;
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
    [RelayCommand]
    private void SelectTerrain()
    {
        
    }
}