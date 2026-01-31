using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Models;
using MolluskEditor.Wrappers;
using MolluskEditor.Commands;
using MolluskEditor.Extensions;
using MolluskEditor.Validators;
using MolluskEngine.GameBoard;
using MolluskEngine.Extensions;

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
        _name = tileset.Name;
        _imageData = tileset.ImageData;
        _terrainData = tileset.TerrainData.ToWrappedStringCollection();
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
    private void SetId(int value) {_tileset.Id = value;}
    private void FixId() {Id = _tileset.Id.ToString();}
    public bool CheckIdAvailable(string idString)
        { return _tilesetData.CheckIdAvailable(idString, _tileset.Id); }
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _imageData;
    [ObservableProperty]
    private ObservableCollection<ObsVal<string>> _terrainData;
    #endregion
    public void Dispose()
    {
        _tilesetData.Data.Remove(int.Parse(Id));
    }
    public void FixFields()
    {
        FixId();
    }
}
