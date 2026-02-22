using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Commands;
using MolluskEditor.Extensions;
using MolluskEditor.Models;
using MolluskEngine;
using MolluskEngine.Extensions;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class MapDataViewModel : ObservableValidator, IDataViewModel
{
    private static CommandStack _commandStack;
    private static DataModel<GameMap> _mapData;
    public static void InjectDependency(
        DataModel<GameMap> mapData, CommandStack commandStack)
    {
        _mapData = mapData;
        _commandStack = commandStack;
    }
    private GameMap _map;

    public MapDataViewModel(GameMap? map)
    {
        map ??= _mapData.New();
        _map = map;
        _id = _map.Id.ToString();
        _name = _map.Name;
        _height = _map.Height.ToString();
        _width = _map.Width.ToString();
        _tileData = _map.TileData.ToMapTileViewModel();

        WatchTileData();
        PropertyChanged += CheckForAnyErrors;
        CheckForAnyErrors(null, EventArgs.Empty);
    }
    public MapDataViewModel() : this(null) { }
    public static ObservableCollection<IDataViewModel> ReadExisting()
    {
        ObservableCollection<IDataViewModel> data = [];
        foreach (GameMap map in _mapData.Data.Values)
            data.Add(new MapDataViewModel(map));
        return data;
    }
    #region Tilemap Painting
    private MapTileViewModel? pickTileWithCursor(Point cursorPosition)
    {
        // Get cursor positions in map coordinates
        int cursorX = (int)(cursorPosition.X / Config.tileWidth);
        int cursorY = (int)(cursorPosition.Y / Config.tileHeight);
        // Return if cursor outside bounds
        if (cursorX < 0 || cursorX >= _map.Width)  {return null;}
        if (cursorY < 0 || cursorY >= _map.Height) {return null;}
        // Index with map coordinates
        return TileData.IndexAs2D(cursorX, cursorY, _map.Width);
    }
    public void PaintTilemap(Point cursorPosition, (int TilesetId, int TileId) id)
    {
        MapTileViewModel? selectedTile = pickTileWithCursor(cursorPosition);
        if (selectedTile == null) return;
        selectedTile.AssignTile(id);
    }
    private CommandSequence? _paintCommands;
    public void BeginPainting()
    {
        _paintCommands = new CommandSequence();
    }
    public void FinishPainting()
    {
        if (_paintCommands == null) return;
        _commandStack.IssueCommand(_paintCommands);
    }
    public Point? CursorToTilemapPosition(Point cursorPosition, int canvasSize)
    {
        Point mapPosition = new Point(
            cursorPosition.X * _map.Width  * Config.tileWidth  / canvasSize, 
            cursorPosition.Y * _map.Height * Config.tileHeight / canvasSize);
        return mapPosition;
    }
    public (int TilesetId, int TileId)? SampleTilemap(Point cursorPosition)
    {
        MapTileViewModel? selectedTile = pickTileWithCursor(cursorPosition);
        if (selectedTile == null) {return null;}
        return selectedTile.Id;
    }
    public int? PickTileIndex(Point cursorPosition)
    {
        MapTileViewModel? selectedTile = pickTileWithCursor(cursorPosition);
        if (selectedTile == null) return null;
        return TileData.IndexOf(selectedTile);
    }
    #region Tilemap Brushes
    public void RefreshTilemapBrushes(Dictionary<int, Bitmap> images)
    {
        foreach (MapTileViewModel mapTile in TileData)
        {
            if (mapTile.TilesetId == null) continue;
            mapTile.RefreshBrush(images[mapTile.TilesetId.Value], _map.Width);
        }
    }
    #endregion
    #endregion
    #region Boilerplate Properties
    [ObservableProperty]
    private string _id;
    public bool CheckIdAvailable(string idString)
        { return _mapData.CheckIdAvailable(idString, _map.Id); }
    private void FixId() {Id = _map.Id.ToString();}
    [ObservableProperty]
    private string _name;
    private void FixName() {Name = _map.Name;}
    [ObservableProperty]
    private string _height;
    private void FixHeight() {Height = _map.Height.ToString();}
    [ObservableProperty]
    private string _width;
    private void FixWidth() {Width = _map.Width.ToString();}
    [ObservableProperty]
    private ObservableCollection<MapTileViewModel> _tileData;
    private void UpdateTileData(object? sender, EventArgs args)
    {
        
    }
    private void FixTileData() {TileData = _map.TileData.ToMapTileViewModel();}
    private void WatchTileData()
    {
        foreach (MapTileViewModel i in TileData)
        {
            i.PropertyChanged += UpdateTileData;
            i.PropertyChanged += CheckForAnyErrors;
        }
    }
    #endregion
    public void Register()
    {
        _mapData.Data[_map.Id] = _map;
    }
    public void Unregister()
    {
        _mapData.Data.Remove(_map.Id);
    }
    public void FixFields()
    {
        FixId();
        FixName();
        FixHeight();
        FixWidth();
        //FixTileData();
    }
    #region Events
    public void NotifyChange()
        { _mapData.OnAnyChange(); }
    /// <summary>
    /// Color of the text in a data selector. Turns yellow if 
    /// there's any errors.
    /// </summary>
    [ObservableProperty]
    private IBrush _textColor = Brush.Parse("White");
    private void CheckForAnyErrors(object? sender, EventArgs args)
    {
        bool anyErrors = Result();
        bool Result()
        {
            if (GetErrors().Any()) return true;
            if (TileData.ToIntList() == null) return true;
            return false;
        }
        if (anyErrors) TextColor = Brush.Parse("Yellow");
        else TextColor = Brush.Parse("White");
    }
    #endregion  
}
