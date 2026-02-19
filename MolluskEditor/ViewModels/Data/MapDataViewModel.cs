using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Commands;
using MolluskEditor.Extensions;
using MolluskEditor.Models;
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
        _tileset = _map.Tileset.ToString();
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
    private string _tileset;
    private void FixTileset() {Tileset = _map.Tileset.ToString();}
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
    private void FixTileData() {/* Todo */}
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
        FixTileset();
        FixTileData();
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
