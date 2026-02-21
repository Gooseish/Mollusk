using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Commands;
using MolluskEditor.Models;
using MolluskEditor.Services;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class MapsEditorViewModel : EditorViewModel
{
    // Todo: "Use png as map" option
    // Todo: "Export as png" option
    private DataModel<GameMap> _dataModel;
    private DataModel<Tileset> _tilesetDataModel;
    [ObservableProperty]
    private DataSelectorViewModel _data;
    [ObservableProperty]
    private MapDataViewModel? _selectedMap;
    [ObservableProperty]
    private DataSelectorViewModel _tilesetData;
    [ObservableProperty]
    private TilesetDataViewModel? _selectedTileset;
    [ObservableProperty]
    private (int TilesetId, int TileId) _selectedTile;
    public MapsEditorViewModel(CommandStack commandStack,
        DataModel<GameMap> dataModel, DataModel<Tileset> tilesetDataModel)
        : base(commandStack)
    {
        _dataModel = dataModel;
        _tilesetDataModel = tilesetDataModel;
        EditorName = EditorName.Maps;
        Data = new(typeof(MapDataViewModel), MapDataViewModel.ReadExisting,
            commandStack);
        TilesetData = new (typeof(TilesetDataViewModel), TilesetDataViewModel.ReadExisting,
            commandStack);
        Subscribe();
    }

    #region Events
    private void OnSelectionChanged(object? sender, EventArgs args)
    {
        SelectedMap = (MapDataViewModel?)Data.SelectedData;
    }
    private void OnSelectedTilesetChanged(object? sender, EventArgs args)
    {
        SelectedTileset = (TilesetDataViewModel?)Data.SelectedData;
    }
    private void OnProjectLoaded(object? sender, EventArgs args)
    {
        Data.Initialize(true);
        TilesetData.Initialize(true);
    }
    private void OnUndoOrRedo(object? sender, EventArgs args)
    {
        int? selectedIndex = SelectedMap == null ? null : int.Parse(SelectedMap.Id);
        Data.Initialize();
        Data.FixIndexAfterUndo(selectedIndex);
    }
    private void RefreshTilsetData(object? sender, EventArgs args)
    {
        int? selectedIndex = SelectedTileset == null ? null : int.Parse(SelectedTileset.Id);
        TilesetData.Initialize();
        TilesetData.FixIndexAfterUndo(selectedIndex);
    }
    private void Subscribe()
    {
        // Subscriptions to singletons/statics (must be manually unsubscribed from)
        SaveLoadService.ProjectLoaded += OnProjectLoaded;
        _dataModel.IdsChanged += Data.SortDataEvent;
        _commandStack.OnUndo += OnUndoOrRedo;
        _commandStack.OnRedo += OnUndoOrRedo;
        _tilesetDataModel.AnyChange += RefreshTilsetData;
        // Subscriptions to transients (events destructed by garbage collector)
        Data.IndexChanged += OnSelectionChanged;
        TilesetData.IndexChanged += OnSelectedTilesetChanged;
    }
    private void Unsubscribe()
    {
        SaveLoadService.ProjectLoaded -= OnProjectLoaded;
        _dataModel.IdsChanged -= Data.SortDataEvent;
        _commandStack.OnUndo -= OnUndoOrRedo;
        _commandStack.OnRedo -= OnUndoOrRedo;
        _tilesetDataModel.AnyChange -= RefreshTilsetData;
    }
    public override void Dispose()
    {
        Unsubscribe();
    }
    #endregion
}
