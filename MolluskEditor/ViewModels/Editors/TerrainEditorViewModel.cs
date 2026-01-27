using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Models;
using MolluskEditor.Services;
using MolluskEngine.Extensions;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class TerrainEditorViewModel : EditorViewModel
{
    [ObservableProperty]
    private TerrainSelectorViewModel _data;
    [ObservableProperty]
    private TerrainDataViewModel? _selectedTerrain;
    public TerrainEditorViewModel(DataModel<Terrain> terrainDataModel)
    {
        EditorName = MolluskEditor.Data.EditorName.Terrain;
        Data = new();
        Subscribe();
    }
    
    #region Event Handling
    private void OnSelectionChanged(object? sender, EventArgs args)
    {
        SelectedTerrain = Data.SelectedData;
    }
    private void OnProjectLoaded(object? sender, EventArgs args)
    {
        Data.Initialize(); // Perhaps abstract this as well
    }
    private void Subscribe()
    {
        SaveLoadService.ProjectLoaded += OnProjectLoaded;
        Data.IndexChanged += OnSelectionChanged;
    }
    private void Unsubscribe()
    {
        SaveLoadService.ProjectLoaded -= OnProjectLoaded;
        Data.IndexChanged -= OnSelectionChanged; // This should be taken care of by the garbage collector, so maybe is unnecessary
    }
    public override void Dispose()
    {
        Unsubscribe();
    }
    #endregion
    
    // Todo: These should be static and readonly. Not sure how to do the binding
    [ObservableProperty]
    private int _weatherCount = WeatherType.Count();
    [ObservableProperty]
    private int _movementCount = MovementType.Count();
    [ObservableProperty]
    private ObservableCollection<string> _weatherNames = WeatherType.Names().ToObservableCollection();
    [ObservableProperty]
    private ObservableCollection<string> _movementNames = MovementType.Names().ToObservableCollection();
}