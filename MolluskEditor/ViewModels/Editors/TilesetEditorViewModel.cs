using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Commands;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;
using MolluskEditor.Services;

namespace MolluskEditor.ViewModels;

public partial class TilesetEditorViewModel : EditorViewModel
{
    private DataModel<Tileset> _dataModel;
    private DataModel<Terrain> _terrainDataModel;
    [ObservableProperty]
    private DataSelectorViewModel _data;
    [ObservableProperty]
    private TilesetDataViewModel? _selectedTileset;
    [ObservableProperty]
    private DataSelectorViewModel _terrainData;
    [ObservableProperty]
    private TerrainDataViewModel? _selectedTerrain;

    public TilesetEditorViewModel(CommandStack commandStack, DataModel<Tileset> dataModel, DataModel<Terrain> terrainDataModel)
        : base(commandStack)
    {
        _dataModel = dataModel;
        _terrainDataModel = terrainDataModel;
        EditorName = EditorName.Tilesets;
        Data = new(typeof(TilesetDataViewModel), TilesetDataViewModel.ReadExisting);
        TerrainData = new(typeof(TerrainDataViewModel), TerrainDataViewModel.ReadExisting, false);
        Subscribe();
    }

    #region Event Handling
    private void OnSelectionChanged(object? sender, EventArgs args)
    {
        SelectedTileset = (TilesetDataViewModel?)Data.SelectedData;
    }
    private void OnSelectedTerrainChanged(object? sender, EventArgs args)
    {
        SelectedTerrain = (TerrainDataViewModel?)TerrainData.SelectedData;
    }
    private void OnProjectLoaded(object? sender, EventArgs args)
    {
        Data.Initialize(); // Perhaps abstract this as well
        TerrainData.Initialize();
    }
    private void OnUndoOrRedo(object? sender, EventArgs args)
    {
        int? selectedIndex = SelectedTileset == null ? null : int.Parse(SelectedTileset.Id);
        Data.Initialize();
        Data.FixIndexAfterUndo(selectedIndex);
    }
    private void RefreshTerrainData(object? sender, EventArgs args)
    {
        int? selectedIndex = SelectedTerrain == null ? null : int.Parse(SelectedTerrain.Id);
        TerrainData.Initialize();
        TerrainData.FixIndexAfterUndo(selectedIndex);
    }
    private void Subscribe()
    {
        // Subscriptions to singletons/statics (must be manually unsubscribed from)
        SaveLoadService.ProjectLoaded += OnProjectLoaded;
        _dataModel.IdsChanged += Data.SortDataEvent;
        _commandStack.OnUndo += OnUndoOrRedo;
        _commandStack.OnRedo += OnUndoOrRedo;
        _terrainDataModel.AnyChange += RefreshTerrainData;
        // Subscriptions to transients (events destructed by garbage collector)
        Data.IndexChanged += OnSelectionChanged;
        TerrainData.IndexChanged += OnSelectedTerrainChanged;
    }
    private void Unsubscribe()
    {
        SaveLoadService.ProjectLoaded -= OnProjectLoaded;
        _dataModel.IdsChanged -= Data.SortDataEvent;
        _commandStack.OnUndo -= OnUndoOrRedo;
        _commandStack.OnRedo -= OnUndoOrRedo;
        _terrainDataModel.AnyChange -= RefreshTerrainData;
    }
    public override void Dispose()
    {
        Unsubscribe();
    }
    #endregion
}
