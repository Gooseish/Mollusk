using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
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

namespace MolluskEditor.ViewModels;

public partial class TilesetDataViewModel : ObservableValidator, IDataViewModel
{
    private static CommandStack _commandStack;
    private static DataModel<Tileset> _tilesetData;
    public static void InjectDependency(DataModel<Tileset> tilesetData, CommandStack commandStack)
    {
        _tilesetData = tilesetData;
        _commandStack = commandStack;
    }
    private Tileset _tileset;
    public TilesetDataViewModel(Tileset? tileset)
    {
        tileset ??= _tilesetData.New();
        _tileset = tileset;
        _id = tileset.Id.ToString();
        _terrainData = tileset.TerrainData.ToTerrainTileViewModel();
        FixImage();
        WatchTerrainData();

        PropertyChanged += CheckForAnyErrors;
        Name = tileset.Name; // Assign to property to trigger filename check
    }
    public TilesetDataViewModel() : this(null) { }

    public static ObservableCollection<IDataViewModel> ReadExisting()
    {
        ObservableCollection<IDataViewModel> data = [];
        foreach (Tileset tileset in _tilesetData.Data.Values)
            data.Add(new TilesetDataViewModel(tileset));
        return data;
    }
    #region Boilerplate Properties
    [ObservableProperty]
    [NotifyDataErrorInfo][ParseAsInt][DontOverrideId]
    private string _id;
    partial void OnIdChanged(string? oldValue, string newValue)
    {
        if (GetErrors(nameof(Id)).Any()) { return; }
        int id = int.Parse(Id);
        if (id == _tileset.Id) { return; }
        CommandSequence command = new();
        command.Add(new MoveInDictCommand(ChangeDictKey, _tileset.Id, id));
        command.Add(new SetCommand<int>(SetId, _tileset.Id, id));
        command.AddCleanup(_tilesetData.OnIdsChanged);
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
        if (Name == _tileset.Name) {return;}
        CommandSequence command = new();
        command.Add(new SetCommand<string>(SetName, _tileset.Name, Name));
        command.AddCleanup(FixImage);
        _commandStack.IssueCommand(command);
    }
    private void SetName(string value)
    {
        _tileset.Name = value;
    }
    [ObservableProperty]
    private Bitmap? _image;
    private void FixImage()
    {
        string full_path = SaveLoadService.CONTENTROOT + SaveLoadService.TILESETIMAGES + _tileset.Name + ".png";
        bool file_exists = File.Exists(full_path);
        if (file_exists)
            Image = new Bitmap(full_path);
    }
    [ObservableProperty]
    private ObservableCollection<TerrainTileViewModel> _terrainData;
    private void UpdateTerrainData(object? sender, EventArgs args)
    {
        List<int>? parsedTerrainData = TerrainData.ToIntList();
        if (parsedTerrainData == null) { return; }
        if (parsedTerrainData == (List<int>)[.. _tileset.TerrainData])
            { return; }
        CommandSequence command = new();
        command.Add(new SetCommand<int[]>(SetTerrainData,
            _tileset.TerrainData, parsedTerrainData.ToArray()));
        command.AddCleanup(FixTerrainData);
        _commandStack.IssueCommand(command);
    }
    private void SetTerrainData(int[] value) {_tileset.TerrainData = value;}
    private void FixTerrainData() {TerrainData = _tileset.TerrainData.ToTerrainTileViewModel();}
    private void WatchTerrainData()
    {
        foreach (TerrainTileViewModel i in TerrainData)
        {
            i.PropertyChanged += UpdateTerrainData;
            i.PropertyChanged += CheckForAnyErrors;
        }
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
    }

    public void NotifyChange()
    {
        _tilesetData.OnAnyChange();
    }

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
