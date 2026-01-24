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
    
    [ObservableProperty]
    private ObservableCollection<TerrainDataViewModel> _terrainData;

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
    }
    [RelayCommand]
    private void SelectTerrain()
    {
        
    }
}