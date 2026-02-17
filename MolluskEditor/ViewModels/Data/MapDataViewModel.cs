using System;
using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Commands;
using MolluskEditor.Models;
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
        
    }
    #region Boilerplate Properties
    [ObservableProperty]
    private string _id;
    public bool CheckIdAvailable(string idString)
        { return _mapData.CheckIdAvailable(idString, _map.Id); }
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _tileset;
    [ObservableProperty]
    private string _height;
    [ObservableProperty]
    private string _width;
    //[ObservableProperty]
    //private ObservableCollection<MapTileViewModel> _tileData;

    #endregion
    public void Register()
    {
        throw new NotImplementedException();
    }
    public void Unregister()
    {
        throw new NotImplementedException();
    }
    public void FixFields()
    {
        throw new NotImplementedException();
    }

    #region Events   
    public void NotifyChange()
    {
        throw new NotImplementedException();
    }
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
            //if (GetErrors().Any()) return true;
            //if (MovementCost.ToIntList() == null) return true;
            return false;
        }
        if (anyErrors) TextColor = Brush.Parse("Yellow");
        else TextColor = Brush.Parse("White");
    }
    #endregion  
}
