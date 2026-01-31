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
    #region Boilerplate Properties
    [ObservableProperty]
    [NotifyDataErrorInfo][ParseAsInt][DontOverrideId]
    private string _id;
    partial void OnIdChanged(string? oldValue, string newValue)
    {
        if (GetErrors(nameof(Id)).Any()) { return; }
        int id = int.Parse(Id);
        if (id == _terrain.Id) { return; }
        CommandSequence command = new();
        command.Add(new MoveInDictCommand(ChangeDictKey, _terrain.Id, id));
        command.Add(new SetCommand<int>(SetId, _terrain.Id, id));
        command.AddCleanup(_terrainData.OnIdsChanged);
        command.AddCleanup(_terrainData.OnAnyChange);
        _commandStack.IssueCommand(command);
    }
    private void SetId(int value) {_terrain.Id = value;}
    private void FixId() {Id = _terrain.Id.ToString();}
    private void ChangeDictKey(int newValue, int oldValue)
    {
        _terrainData.Data.Remove(oldValue);
        _terrainData.Data[newValue] = _terrain;
    }
    public bool CheckIdAvailable(string idString)
        { return _terrainData.CheckIdAvailable(idString, _terrain.Id); }
    [ObservableProperty]
    private string _name;
    partial void OnNameChanged(string? oldValue, string newValue)
    {
        CommandSequence command = new();
        command.Add(new SetCommand<string>(SetName, oldValue, newValue));
        command.AddCleanup(_terrainData.OnAnyChange);
        _commandStack.IssueCommand(command);
    }
    private void SetName(string value) {_terrain.Name = value;}
    [ObservableProperty]
    [NotifyDataErrorInfo][ParseAsInt]
    private string _avoid;
    partial void OnAvoidChanged(string? oldValue, string newValue)
    {
        if (GetErrors(nameof(Avoid)).Any()) { return; }
        int avoid = int.Parse(Avoid);
        if (avoid == _terrain.Avoid) { return; }
        CommandSequence command = new();
        command.Add(new SetCommand<int>(SetAvo, _terrain.Avoid, avoid));
        command.AddCleanup(_terrainData.OnAnyChange);
        _commandStack.IssueCommand(command);
    }
    private void SetAvo(int value) { _terrain.Avoid = value; }
    private void FixAvoid() {Avoid = _terrain.Avoid.ToString();}
    [ObservableProperty]
    [NotifyDataErrorInfo][ParseAsInt]
    private string _def;
    partial void OnDefChanged(string? oldValue, string newValue)
    {
        if (GetErrors(nameof(Def)).Any()) { return; }
        int def = int.Parse(Def);
        if (def == _terrain.Def) { return; }
        CommandSequence command = new();
        command.Add(new SetCommand<int>(SetDef, _terrain.Def, def));
        command.AddCleanup(_terrainData.OnAnyChange);
        _commandStack.IssueCommand(command);
    }
    private void SetDef(int value) {_terrain.Def = value;}
    private void FixDef() {Def = _terrain.Def.ToString();}
    [ObservableProperty]
    [NotifyDataErrorInfo][ParseAsInt]
    private string _res;
    partial void OnResChanged(string? oldValue, string newValue)
    {
        if (GetErrors(nameof(Res)).Any()) { return; }
        int res = int.Parse(Res);
        if (res == _terrain.Res) { return; }
        SetCommand<int> command = new(SetRes, _terrain.Res, res);
        _commandStack.IssueCommand(command);
    }
    private void SetRes(int value) { _terrain.Res = value;}
    private void FixRes() { Res = _terrain.Res.ToString();}
    [ObservableProperty]
    [NotifyDataErrorInfo][ParseAsInt]
    private string _healPercent;
    partial void OnHealPercentChanged(string? oldValue, string newValue)
    {
        if (GetErrors(nameof(HealPercent)).Any()) { return; }
        int healPercent = int.Parse(HealPercent);
        if (healPercent == _terrain.HealPercent) { return; }
        CommandSequence command = new();
        command.Add(new SetCommand<int>(SetHealPercent, _terrain.HealPercent, healPercent));
        command.AddCleanup(_terrainData.OnAnyChange);
        _commandStack.IssueCommand(command);
    }
    private void SetHealPercent(int value) {_terrain.HealPercent = value;}
    private void FixHealPercent() { HealPercent = _terrain.HealPercent.ToString();}
    [ObservableProperty]
    private ObservableCollection<ObsVal<string>> _movementCost;
    private void UpdateMovementCost(object? sender, EventArgs eventArgs)
    {
        List<int>? parsedMovementCost = MovementCost.ToIntList();
        if (parsedMovementCost == null) { return; }
        if (parsedMovementCost == (List<int>)[.. _terrain.MovementCost]) 
            { return; }
        CommandSequence command = new();
        command.Add(new SetCommand<int[,]>(SetMovementCost, 
            _terrain.MovementCost, GetMovementCostArray(parsedMovementCost)));
        command.AddCleanup(_terrainData.OnAnyChange);
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
    private void FixMovementCost() { MovementCost = GetMovementCostCollection(_terrain.MovementCost); }
    private void WatchMovementCosts()
    {
        foreach (ObsVal<string> i in MovementCost)
            i.PropertyChanged += UpdateMovementCost;
    }
    #endregion
    public void Dispose()
    {
        _terrainData.Data.Remove(int.Parse(Id));
        _terrainData.OnAnyChange();
    }
    public void FixFields()
    {
        FixId();
        FixAvoid();
        FixDef();
        FixRes();
        FixHealPercent();
        FixMovementCost();
    }

    public void OnAdded()
    {
        _terrainData.OnAnyChange();
    }
}