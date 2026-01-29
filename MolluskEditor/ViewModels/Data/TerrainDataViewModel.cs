using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;
using MolluskEngine.Extensions;
using MolluskEditor.Wrappers;
using System.Diagnostics;
using MolluskEditor.Commands;

namespace MolluskEditor.ViewModels;

public partial class TerrainDataViewModel : ObservableObject, IDataViewModel
{
    private static CommandStack _commandStack;
    private static DataModel<Terrain>? _terrainData;
    public static void InjectDependency(DataModel<Terrain> terrainData, CommandStack commandStack)
    {
        _terrainData = terrainData;
        _commandStack = commandStack;
    }
    /// <summary>
    /// Create a new TerrainDataViewModel by creating a new
    /// terrain instance and registering it in the dictionary
    /// of all terrain data.
    /// </summary>
    public TerrainDataViewModel(Terrain? terrain)
    {
        Debug.Assert(_terrainData != null, 
            "Tried to create terrain data viewmodel without reference to datamodel singleton.");
        terrain ??= _terrainData.New();
        _id = terrain.Id;
        _name = terrain.Name;
        _avoid = terrain.Avoid;
        _def = terrain.Def;
        _res = terrain.Res;
        _healPercent = terrain.HealPercent;
        _movementCost = GetMovementCostCollection(terrain.MovementCost);
        WatchMovementCosts();
    }
    public TerrainDataViewModel() : this(null) { }
    public static ObservableCollection<IDataViewModel> ReadExisting()
    {
        Debug.Assert(_terrainData != null, 
            "Tried to read terrain data viewmodel without reference to datamodel singleton.");

        ObservableCollection<IDataViewModel> data = [];
        foreach (Terrain terrain in _terrainData.Data.Values)
            data.Add(new TerrainDataViewModel(terrain));
        return data;
    }
    public void WatchMovementCosts()
    {
        foreach (ObsVal<int> i in MovementCost)
            i.PropertyChanged += UpdateMovementCost;
    }
    #region Boilerplate Properties
    private Terrain _terrain {get {return _terrainData.Data[Id];}}
    [ObservableProperty]
    private int _id;
    partial void OnIdChanged(int oldValue, int newValue)
    {
        _terrainData.Data.Remove(oldValue);
        _terrainData.Data[newValue] = GetTerrain();
    }
    [ObservableProperty]
    private string _name;
    partial void OnNameChanged(string? oldValue, string newValue)
    {
        SetCommand<string> command = new(SetName, oldValue, newValue);
        _commandStack.IssueCommand(command);
    }
    private void SetName(string value) {_terrain.Name = value;}
    [ObservableProperty]
    private int _avoid;
    partial void OnAvoidChanged(int oldValue, int newValue)
    {
        SetCommand<int> command = new(SetAvo, oldValue, newValue);
        _commandStack.IssueCommand(command);
    }
    private void SetAvo(int value) { _terrain.Avoid = value; }
    [ObservableProperty]
    private int _def;
    partial void OnDefChanged(int oldValue, int newValue)
    {
        SetCommand<int> command = new(SetDef, oldValue, newValue);
        _commandStack.IssueCommand(command);
    }
    private void SetDef(int value) {_terrain.Def = value;}
    [ObservableProperty]
    private int _res;
    partial void OnResChanged(int oldValue, int newValue)
    {
        SetCommand<int> command = new(SetRes, oldValue, newValue);
        _commandStack.IssueCommand(command);
    }
    private void SetRes(int value) { _terrain.Res = value;}
    [ObservableProperty]
    private int _healPercent;
    partial void OnHealPercentChanged(int oldValue, int newValue)
    {
        SetCommand<int> command = new(SetHealPercent, oldValue, newValue);
        _commandStack.IssueCommand(command);
    }
    private void SetHealPercent(int value) {_terrain.HealPercent = value;}
    [ObservableProperty]
    private ObservableCollection<ObsVal<int>> _movementCost;
    public void UpdateMovementCost(object? sender, EventArgs eventArgs)
    {
        SetCommand<int[,]> command = new(SetMovementCost, _terrain.MovementCost, GetMovementCostArray(MovementCost));
        _commandStack.IssueCommand(command);
    }
    private void SetMovementCost(int[,] value) {_terrain.MovementCost = value;}
    private static int[,] GetMovementCostArray(
        ObservableCollection<ObsVal<int>> movementCostCollection)
    {
        var unwrappedCollection = movementCostCollection.Select(n => n.Value);
        return unwrappedCollection.To2DArray(WeatherType.Count(), MovementType.Count());
    }
    private static ObservableCollection<ObsVal<int>> GetMovementCostCollection(
        int[,] movementCostArray)
    {
        ObservableCollection<int> unwrappedCollection = [.. movementCostArray]; // What the hell is this syntax?
        return unwrappedCollection.Select(n => new ObsVal<int>(n)).ToObservableCollection();
    }
    #endregion
    public Terrain GetTerrain()
    {
        return new Terrain()
        {
            Id = Id,
            Name = Name,
            Avoid = Avoid,
            Def = Def,
            Res = Res,
            HealPercent = HealPercent,
            MovementCost = GetMovementCostArray(MovementCost),
        };
    }
    public void Dispose()
    {
        _terrainData.Data.Remove(Id);
    }
}