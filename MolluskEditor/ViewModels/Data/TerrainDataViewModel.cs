using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;
using MolluskEngine.Extensions;
using MolluskEditor.Wrappers;
using MolluskEditor.Commands;
using System.Collections.Generic;
using MolluskEditor.Extensions;
using MolluskEditor.Validators;

namespace MolluskEditor.ViewModels;

public partial class TerrainDataViewModel : ObservableValidator, IDataViewModel
{
    private static CommandStack _commandStack;
    private static DataModel<Terrain> _terrainData;
    public static void InjectDependency(DataModel<Terrain> terrainData, CommandStack commandStack)
    {
        _terrainData = terrainData;
        _commandStack = commandStack;
    }
    private Terrain _terrain;
    /// <summary>
    /// Create a new TerrainDataViewModel by creating a new
    /// terrain instance and registering it in the dictionary
    /// of all terrain data.
    /// </summary>
    public TerrainDataViewModel(Terrain? terrain)
    {
        terrain ??= _terrainData.New();
        _terrain = terrain;
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
        ObservableCollection<IDataViewModel> data = [];
        foreach (Terrain terrain in _terrainData.Data.Values)
            data.Add(new TerrainDataViewModel(terrain));
        return data;
    }
    public void WatchMovementCosts()
    {
        foreach (ObsVal<string> i in MovementCost)
            i.PropertyChanged += UpdateMovementCost;
    }
    #region Boilerplate Properties
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [ParseAsInt]
    [DontOverrideId]
    // Needs validator to check id list
    private string _id;
    partial void OnIdChanged(string? oldValue, string newValue)
    {
        if (GetErrors(nameof(Id)).Any()) { return; }
        int id = int.Parse(Id);
        // Todo: command stack
        _terrainData.Data.Remove(_terrain.Id);
        _terrain.Id = id;
        _terrainData.Data[id] = _terrain;
    }
    public bool CheckIdAvailable(string idString)
        { return _terrainData.CheckIdAvailable(idString, _terrain.Id); }
    [ObservableProperty]
    private string _name;
    partial void OnNameChanged(string? oldValue, string newValue)
    {
        SetCommand<string> command = new(SetName, oldValue, newValue);
        _commandStack.IssueCommand(command);
    }
    private void SetName(string value) {_terrain.Name = value;}
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [ParseAsInt]
    private string _avoid;
    partial void OnAvoidChanged(string? oldValue, string newValue)
    {
        if (GetErrors(nameof(Avoid)).Any()) { return; }
        int avoid = int.Parse(Avoid);
        SetCommand<int> command = new(SetAvo, _terrain.Avoid, avoid);
        _commandStack.IssueCommand(command);
    }
    private void SetAvo(int value) { _terrain.Avoid = value; }
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [ParseAsInt]
    private string _def;
    partial void OnDefChanged(string? oldValue, string newValue)
    {
        if (GetErrors(nameof(Def)).Any()) { return; }
        int def = int.Parse(Def);
        SetCommand<int> command = new(SetDef, _terrain.Def, def);
        _commandStack.IssueCommand(command);
    }
    private void SetDef(int value) {_terrain.Def = value;}
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [ParseAsInt]
    private string _res;
    partial void OnResChanged(string? oldValue, string newValue)
    {
        if (GetErrors(nameof(Res)).Any()) { return; }
        int res = int.Parse(Res);
        SetCommand<int> command = new(SetRes, _terrain.Res, res);
        _commandStack.IssueCommand(command);
    }
    private void SetRes(int value) { _terrain.Res = value;}
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [ParseAsInt]
    private string _healPercent;
    partial void OnHealPercentChanged(string? oldValue, string newValue)
    {
        if (GetErrors(nameof(HealPercent)).Any()) { return; }
        int healPercent = int.Parse(HealPercent);
        SetCommand<int> command = new(SetHealPercent, _terrain.HealPercent, healPercent);
        _commandStack.IssueCommand(command);
    }
    private void SetHealPercent(int value) {_terrain.HealPercent = value;}
    [ObservableProperty]
    private ObservableCollection<ObsVal<string>> _movementCost;
    public void UpdateMovementCost(object? sender, EventArgs eventArgs)
    {
        List<int>? parsedMovementCost = MovementCost.ToIntList();
        if (parsedMovementCost == null)
        {
            return;
        }
        SetCommand<int[,]> command = new(SetMovementCost, _terrain.MovementCost, GetMovementCostArray(parsedMovementCost));
        _commandStack.IssueCommand(command);
    }
    private void SetMovementCost(int[,] value) {_terrain.MovementCost = value;}
    private static int[,] GetMovementCostArray(
        List<int> movementCostList)
    {
        return movementCostList.To2DArray(WeatherType.Count(), MovementType.Count());
    }
    private static ObservableCollection<ObsVal<string>> GetMovementCostCollection(
        int[,] movementCostArray)
    {
        return movementCostArray.ToWrappedStringCollection();
    }
    #endregion
    public void Dispose()
    {
        _terrainData.Data.Remove(int.Parse(Id));
    }
}