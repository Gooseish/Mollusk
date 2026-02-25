using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Commands;
using MolluskEditor.Models;
using MolluskEditor.Services;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class MapsEditorViewModel : EditorViewModel
{
    // Todo: "Use png as map" option
    // Todo: "Export as png" option
    #region Data Fields
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
    private Tile? _selectedTile;
    #endregion
    #region Constructor
    public MapsEditorViewModel(CommandStack commandStack,
        DataModel<GameMap> dataModel, DataModel<Tileset> tilesetDataModel)
        : base(commandStack)
    {
        _dataModel = dataModel;
        _tilesetDataModel = tilesetDataModel;
        EditorName = EditorName.Maps;
        Data = new(typeof(MapDataViewModel), MapDataViewModel.ReadExisting,
            commandStack);
        TilesetData = new(typeof(TilesetDataViewModel), TilesetDataViewModel.ReadExisting,
            commandStack, false);
        Subscribe();
        RefreshTilesetImages();
    }
    #endregion
    #region Tile Picker
    [ObservableProperty]
    public static int _tilesetCanvasSize = 300;
    public void PickTile(Point cursorPositionOnTilemap, Point cursorPositionInBorder)
    {
        if (cursorPositionInBorder.X < 0 || cursorPositionInBorder.X >= TilesetCanvasSize) return;
        if (cursorPositionInBorder.Y < 0 || cursorPositionInBorder.Y >= TilesetCanvasSize) return;
        if (SelectedTileset == null) return;
        Point? mapPosition = SelectedTileset.CursorToTilemapPosition(cursorPositionOnTilemap, TilesetCanvasSize);
        if (mapPosition == null) return;
        int? tileId = SelectedTileset.PickTileIndex((Point)mapPosition);
        if (tileId == null) return;
        SelectedTile = new Tile(int.Parse(SelectedTileset.Id), (int)tileId);   
    }
    #endregion
    #region Tilemap Painting
    [ObservableProperty]
    public static int _canvasWidth = 500;
    [ObservableProperty]
    public static int _canvasHeight = 400;
    public void PaintTilemap(Point cursorPositionOnTilemap, Point cursorPositionInBorder)
    {
        if (cursorPositionInBorder.X < 0 || cursorPositionInBorder.X >= CanvasWidth) return;
        if (cursorPositionInBorder.Y < 0 || cursorPositionInBorder.Y >= CanvasHeight) return;
        if (SelectedMap == null) return;
        if (SelectedTile == null) return;
        Point? mapPosition = SelectedMap?.CursorToTilemapPosition(cursorPositionOnTilemap);
        if (mapPosition == null) return;
        SelectedMap?.PaintTilemap((Point)mapPosition, SelectedTile.Value);
    }
    public void BeginPainting()
    {
        SelectedMap?.BeginPainting();
    }
    public void FinishPainting()
    {
        SelectedMap?.FinishPainting();
    }
    public void SampleTilemap(Point cursorPosition)
    {
        if (SelectedMap == null) return;
        Point? mapPosition = SelectedMap?.CursorToTilemapPosition(cursorPosition);
        if (mapPosition == null) return;
        Tile? sampledTileId = 
            SelectedMap?.SampleTilemap((Point)mapPosition);
        if (sampledTileId == null) return;
        SelectedTile = sampledTileId;
    }
    #endregion
    #region Tilemap Rendering
    private Dictionary<int, Bitmap> _tilesetImages;
    private void RefreshTilesetImages()
    {
        _tilesetImages = [];
        foreach (Tileset tilesetData in _tilesetDataModel.Data.Values)
        {
            string full_path = SaveLoadService.CONTENTROOT 
                             + SaveLoadService.TILESETIMAGES 
                             + tilesetData.Name + ".png";
            if (!File.Exists(full_path)) continue;
            _tilesetImages[tilesetData.Id] = new Bitmap(full_path);
        }
        MapTileViewModel.InjectDependency(_tilesetImages);
        SelectedMap?.RefreshTilemapBrushes();
    }
    #endregion
    #region Tilemap Resizing
    [RelayCommand]
    private void ResizeTilemap()
    {
        SelectedMap?.ResizeTilemap();
    }
    #endregion
    #region Events
    private void OnSelectionChanged(object? sender, EventArgs args)
    {
        SelectedMap = (MapDataViewModel?)Data.SelectedData;
        RefreshTilesetImages();
    }
    private void OnSelectedTilesetChanged(object? sender, EventArgs args)
    {
        SelectedTileset = (TilesetDataViewModel?)TilesetData.SelectedData;
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
        RefreshTilesetImages();
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
