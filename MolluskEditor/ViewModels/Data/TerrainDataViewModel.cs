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
    private static DataModel<Terrain>? _terrainData; // Why is this nullable?
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
        _id = terrain.Id.ToString();
        _name = terrain.Name;
        _avoid = terrain.Avoid.ToString();
        _def = terrain.Def.ToString();
        _res = terrain.Res.ToString();
        _healPercent = terrain.HealPercent.ToString();
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
    private Terrain _terrain {get {return _terrainData.Data[int.Parse(Id)];}}
    [ObservableProperty]
    private string _id;
    partial void OnIdChanged(string? oldValue, string newValue)
    {
        // Todo: validate
        // Todo: command stack
        _terrainData.Data.Remove(int.Parse(oldValue));
        _terrainData.Data[int.Parse(newValue)] = GetTerrain();
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
    private string _avoid;
    partial void OnAvoidChanged(string? oldValue, string newValue)
    {
        if (!int.TryParse(Avoid, out int avoid)) 
        {
            Avoid = oldValue ?? "";
            return;
        }
        SetCommand<int> command = new(SetAvo, _terrain.Avoid, avoid);
        _commandStack.IssueCommand(command);
    }
    private void SetAvo(int value) { _terrain.Avoid = value; }
    [ObservableProperty]
    private string _def;
    partial void OnDefChanged(string? oldValue, string newValue)
    {
        if (!int.TryParse(Def, out int def)) // Validate 
        {
            Def = oldValue ?? "";
            return;
        }
        SetCommand<int> command = new(SetDef, _terrain.Def, def);
        _commandStack.IssueCommand(command);
    }
    private void SetDef(int value) {_terrain.Def = value;}
    [ObservableProperty]
    private string _res;
    partial void OnResChanged(string? oldValue, string newValue)
    {
        if (!int.TryParse(Res, out int res))
        {
            Res = oldValue ?? "";
            return;
        }
        SetCommand<int> command = new(SetRes, _terrain.Res, res);
        _commandStack.IssueCommand(command);
    }
    private void SetRes(int value) { _terrain.Res = value;}
    [ObservableProperty]
    private string _healPercent;
    partial void OnHealPercentChanged(string? oldValue, string newValue)
    {
        if (!int.TryParse(HealPercent, out int healPercent))
            {
                HealPercent = oldValue ?? "";
                return;
            }
        SetCommand<int> command = new(SetHealPercent, _terrain.HealPercent, healPercent);
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
            Id = int.Parse(Id),
            Name = Name,
            Avoid = int.Parse(Avoid),
            Def = int.Parse(Def),
            Res = int.Parse(Res),
            HealPercent = int.Parse(HealPercent),
            MovementCost = GetMovementCostArray(MovementCost),
        };
    }
    public void Dispose()
    {
        _terrainData?.Data.Remove(int.Parse(Id));
    }
}