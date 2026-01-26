using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Data;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;
using MolluskEngine.Extensions;
using MolluskEditor.Wrappers;

namespace MolluskEditor.ViewModels;

public partial class TerrainDataViewModel : ObservableObject, IDataViewModel
{
    private TerrainDataModel _terrainData;
    /// <summary>
    /// Create a new TerrainDataViewModel by creating a new
    /// terrain instance and registering it in the dictionary
    /// of all terrain data.
    /// </summary>
    public TerrainDataViewModel(TerrainDataModel terrainData, Terrain? terrain = null)
    {
        _terrainData = terrainData;
        terrain ??= _terrainData.NewTerrain();
        _id = terrain.Id;
        _name = terrain.Name;
        _avoid = terrain.Avoid;
        _def = terrain.Def;
        _res = terrain.Res;
        _heals = terrain.Heals;
        _healPercent = terrain.HealPercent;
        _movementCost = GetMovementCostCollection(terrain.MovementCost);
        WatchMovementCosts();
    }
    public void WatchMovementCosts()
    {
        foreach (ObsVal<int> i in MovementCost)
            i.PropertyChanged += UpdateMovementCost;
    }
    #region Boilerplate Properties
    private Terrain _terrain {get {return (Terrain)_terrainData.TerrainData[Id];}}
    [ObservableProperty]
    private int _id;
    partial void OnIdChanged(int oldValue, int newValue)
    {
        _terrainData.TerrainData.Remove(oldValue);
        _terrainData.TerrainData[newValue] = GetTerrain();
    }
    [ObservableProperty]
    private string _name;
    partial void OnNameChanged(string? oldValue, string newValue)
    {
        _terrain.Name = newValue;
    }
    [ObservableProperty]
    private int _avoid;
    partial void OnAvoidChanged(int oldValue, int newValue)
    {
        _terrain.Avoid = newValue;
    }
    [ObservableProperty]
    private int _def;
    partial void OnDefChanged(int oldValue, int newValue)
    {
        _terrain.Def = newValue;
    }
    [ObservableProperty]
    private int _res;
    partial void OnResChanged(int oldValue, int newValue)
    {
        _terrain.Res = newValue;
    }
    [ObservableProperty]
    private bool _heals;
    partial void OnHealsChanged(bool oldValue, bool newValue)
    {
        _terrain.Heals = newValue;
    }
    [ObservableProperty]
    private int _healPercent;
    partial void OnHealPercentChanged(int oldValue, int newValue)
    {
        _terrain.HealPercent = newValue;
    }
    [ObservableProperty]
    private ObservableCollection<ObsVal<int>> _movementCost;
    partial void OnMovementCostChanged(
        ObservableCollection<ObsVal<int>>? oldValue,
        ObservableCollection<ObsVal<int>> newValue)
    {
        // This presently doesn't do anything because it's never called. (?)
        // UpdateMovementCost is what actually updates the data model.
        _terrain.MovementCost = GetMovementCostArray(newValue);
    }
    public void UpdateMovementCost(object? sender, EventArgs eventArgs)
    {
        _terrain.MovementCost = GetMovementCostArray(MovementCost);
    }
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
            Heals = Heals,
            HealPercent = HealPercent,
            MovementCost = GetMovementCostArray(MovementCost),
        };
    }
    public void Dispose()
    {
        _terrainData.TerrainData.Remove(Id);
    }
}
