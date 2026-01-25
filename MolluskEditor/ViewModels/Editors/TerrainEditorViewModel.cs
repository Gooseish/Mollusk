using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Models;
using MolluskEditor.Services;

namespace MolluskEditor.ViewModels;

public partial class TerrainEditorViewModel : EditorViewModel
{
    [ObservableProperty]
    private DataSelectorSidebarViewModel _data;
    [ObservableProperty]
    private TerrainDataViewModel? _selectedTerrain;
    public TerrainEditorViewModel()
    {
        EditorName = MolluskEditor.Data.EditorName.Terrain;
        Data = new DataSelectorSidebarViewModel();
        Data.Initialize();
        Subscribe();
    }
    
    #region Event Handling
    private void OnSelectionChanged(object? sender, EventArgs args)
    {
        SelectedTerrain = (TerrainDataViewModel?)Data.SelectedData;
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
}