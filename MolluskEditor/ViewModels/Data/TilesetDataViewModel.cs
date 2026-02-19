using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Models;
using MolluskEditor.Commands;
using MolluskEditor.Extensions;
using MolluskEditor.Validators;
using MolluskEngine.GameBoard;
using Avalonia.Media.Imaging;
using MolluskEditor.Services;
using Avalonia.Media;
using Avalonia;
using MolluskEngine;
using MolluskEngine.Extensions;

namespace MolluskEditor.ViewModels;

public partial class TilesetDataViewModel : ObservableValidator, IDataViewModel
{
    #region Bitmaps
    #endregion
    private static CommandStack _commandStack;
    private static DataModel<Tileset> _tilesetData;
    public static void InjectDependency(
        DataModel<Tileset> tilesetData, CommandStack commandStack)
    {
        _tilesetData = tilesetData;
        _commandStack = commandStack;
    }
    private Tileset _tileset;
    private int? _tilemapWidth;
    private int? _tilemapHeight;
    public TilesetDataViewModel(Tileset? tileset)
    {
        tileset ??= _tilesetData.New();
        _tileset = tileset;
        _id = tileset.Id.ToString();
        _terrainData = tileset.TerrainData.ToTerrainTileViewModel();
        Name = tileset.Name;
        FixImage();
        WatchTerrainData();

        PropertyChanged += CheckForAnyErrors;
        CheckForAnyErrors(null, EventArgs.Empty);
    }
    public TilesetDataViewModel() : this(null) { }

    public static ObservableCollection<IDataViewModel> ReadExisting()
    {
        ObservableCollection<IDataViewModel> data = [];
        foreach (Tileset tileset in _tilesetData.Data.Values)
            data.Add(new TilesetDataViewModel(tileset));
        return data;
    }
    #region Tilemap Painting
    private TerrainTileViewModel? pickTileWithCursor(Point cursorPosition)
    {
        if (Image == null) {return null;}
        if (_tilemapWidth == null) {return null;}
        if (_tilemapHeight == null) {return null;}
        // Get cursor positions in map coordinates
        int cursorX = (int)(cursorPosition.X / Config.tileWidth);
        int cursorY = (int)(cursorPosition.Y / Config.tileHeight);
        // Return if cursor outside bounds
        if (cursorX < 0 || cursorX >= _tilemapWidth)  {return null;}
        if (cursorY < 0 || cursorY >= _tilemapHeight) {return null;}
        // Index with map coordinates
        return TerrainData.IndexAs2D(cursorX, cursorY, (int)_tilemapWidth);
    }
    public void PaintTilemap(Point cursorPosition, string selectedTerrain)
    {
        TerrainTileViewModel? selectedTile = pickTileWithCursor(cursorPosition);
        if (selectedTile == null) return;
        selectedTile.AssignTerrain(selectedTerrain);
    }
    private CommandSequence? _paintCommands;
    public void BeginPainting()
    {
        if (Image == null) return;
        _paintCommands = new CommandSequence();
    }
    public void FinishPainting()
    {
        if (Image == null) return;
        if (_paintCommands == null) return;
        //_paintCommands.AddCleanup(FixTerrainData);
        _commandStack.IssueCommand(_paintCommands);
    }
    public int? SampleTilemap(Point cursorPosition)
    {
        TerrainTileViewModel? selectedTile = pickTileWithCursor(cursorPosition);
        if (selectedTile == null) {return null;}
        return selectedTile.Id;
    }
    #endregion

    #region Boilerplate Properties
    [ObservableProperty]
    [NotifyDataErrorInfo][ParseAsInt][DontOverrideId]
    private string _id;
    partial void OnIdChanged(string? oldValue, string newValue)
    {
        if (GetErrors(nameof(Id)).Any()) return;
        int id = int.Parse(Id);
        if (id == _tileset.Id) return;
        CommandSequence command = new();
        command.Add(new MoveInDictCommand(ChangeDictKey, _tileset.Id, id));
        command.Add(new SetCommand<int>(SetId, _tileset.Id, id));
        command.AddCleanup(_tilesetData.OnIdsChanged);
        command.AddCleanup(FixId);
        _commandStack.IssueCommand(command);
    }
    private void SetId(int value) {_tileset.Id = value;}
    private void FixId() {Id = _tileset.Id.ToString();}
    private void ChangeDictKey(int newValue, int oldValue)
    {
        _tilesetData.Data.Remove(oldValue);
        _tilesetData.Data[newValue] = _tileset;
    }
    public bool CheckIdAvailable(string idString)
        { return _tilesetData.CheckIdAvailable(idString, _tileset.Id); }
    [ObservableProperty]
    [NotifyDataErrorInfo][MatchFilename("Tileset image not found", 
        @"C:/Users/Home/Documents/Monogame_Projects/Mollusk/MolluskEngine/Content/Graphics/Tilesets/")] // Todo: fix hardcoding
    private string _name;
    // Needs validation to make sure image name is correct
    partial void OnNameChanged(string? oldValue, string newValue)
    {
        if (Name == _tileset.Name) return;
        CommandSequence command = new();
        command.Add(new SetCommand<string>(SetName, _tileset.Name, Name));
        command.AddCleanup(FixName);
        command.AddCleanup(FixImage);
        _commandStack.IssueCommand(command);
    }
    private void SetName(string value)
    {
        _tileset.Name = value;
    }
    private void FixName() {Name = _tileset.Name;}
    [ObservableProperty]
    private Bitmap? _image;
    private void FixImage()
    {
        string full_path = SaveLoadService.CONTENTROOT + SaveLoadService.TILESETIMAGES + _tileset.Name + ".png";
        if (!File.Exists(full_path)) return;
        Image = new Bitmap(full_path);
        _tilemapWidth = Image.PixelSize.Width / Config.tileWidth; 
        _tilemapHeight = Image.PixelSize.Height / Config.tileHeight;
    }
    
    [ObservableProperty]
    private ObservableCollection<TerrainTileViewModel> _terrainData;
    private void UpdateTerrainData(object? sender, EventArgs args)
    {
        /*
        CommandSequence command = new();
        command.Add(new SetCommand<int[]>(SetTerrainData,
            _tileset.TerrainData, parsedTerrainData.ToArray()));
        command.AddCleanup(FixTerrainData);
        _commandStack.IssueCommand(command);
        */
        if (sender == null 
            || sender is not TerrainTileViewModel terrainTile)
            return;
        int n = terrainTile.Index;
        if (TerrainData[n].Id == null) return;
        if (TerrainData[n].Id == _tileset.TerrainData[n]) return;
        SetInCollectionCommand<int> command = new(
            SetTerrainData, n, _tileset.TerrainData[n], (int)TerrainData[n].Id);
        _paintCommands?.Add(command);
    }
    private void SetTerrainData(int n, int value) {_tileset.TerrainData[n] = value;}
    private void FixTerrainData() {TerrainData = _tileset.TerrainData.ToTerrainTileViewModel();}
    private void WatchTerrainData()
    {
        foreach (TerrainTileViewModel i in TerrainData)
        {
            i.PropertyChanged += UpdateTerrainData;
            i.PropertyChanged += CheckForAnyErrors;
        }
    }
    public void RefreshColors()
    {
        foreach (TerrainTileViewModel tile in TerrainData)
            tile.RefreshColor();
    }
    #endregion
    public void Register()
    {
        _tilesetData.Data[_tileset.Id] = _tileset;
    }
    public void Unregister()
    {
        _tilesetData.Data.Remove(_tileset.Id);
    }
    public void FixFields()
    {
        FixId();
        FixTerrainData();
        FixName();
    }

    public void NotifyChange()
        { _tilesetData.OnAnyChange(); }

    [ObservableProperty]
    private IBrush _textColor = Brush.Parse("White");
    private void CheckForAnyErrors(object? sender, EventArgs args)
    {
        bool anyErrors = Result();
        bool Result()
        {
            if (GetErrors().Any()) return true;
            if (TerrainData.ToIntList() == null) return true;
            return false;
        }
        if (anyErrors) TextColor = Brush.Parse("Yellow");
        else TextColor = Brush.Parse("White");
    }
}
