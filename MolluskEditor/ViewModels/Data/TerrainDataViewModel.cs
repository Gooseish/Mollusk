using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class TerrainDataViewModel : ObservableObject
{
    /// <summary>
    /// Create a new TerrainDataViewModel by creating a new
    /// terrain instance and registering it in the dictionary
    /// of all terrain data.
    /// </summary>
    public TerrainDataViewModel()
    {
        Terrain terrain = TerrainDataModel.newTerrain();
        _id = terrain.Id;
        _name = terrain.Name;
        _avoid = terrain.Avoid;
        _def = terrain.Def;
        _res = terrain.Res;
        _heals = terrain.Heals;
        _healPercent = terrain.HealPercent;
        _movementCost = terrain.MovementCost;
    }
    public TerrainDataViewModel(Terrain terrain)
    {
        _id = terrain.Id;
        _name = terrain.Name;
        _avoid = terrain.Avoid;
        _def = terrain.Def;
        _res = terrain.Res;
        _heals = terrain.Heals;
        _healPercent = terrain.HealPercent;
        _movementCost = terrain.MovementCost;
    }
    #region Boilerplate Properties
    [ObservableProperty]
    private int _id;
    partial void OnIdChanged(int oldValue, int newValue)
    {
        TerrainDataModel.TerrainData.Remove(oldValue);
        TerrainDataModel.TerrainData[newValue] = GetTerrain();
    }
    [ObservableProperty]
    private string _name;
    partial void OnNameChanged(string? oldValue, string newValue)
    {
        TerrainDataModel.TerrainData[Id].Name = newValue;
    }
    [ObservableProperty]
    private int _avoid;
    partial void OnAvoidChanged(int oldValue, int newValue)
    {
        TerrainDataModel.TerrainData[Id].Avoid = newValue;
    }
    [ObservableProperty]
    private int _def;
    partial void OnDefChanged(int oldValue, int newValue)
    {
        TerrainDataModel.TerrainData[Id].Def = newValue;
    }
    [ObservableProperty]
    private int _res;
    partial void OnResChanged(int oldValue, int newValue)
    {
        TerrainDataModel.TerrainData[Id].Res = newValue;
    }
    [ObservableProperty]
    private bool _heals;
    partial void OnHealsChanged(bool oldValue, bool newValue)
    {
        TerrainDataModel.TerrainData[Id].Heals = newValue;
    }
    [ObservableProperty]
    private int _healPercent;
    partial void OnHealPercentChanged(int oldValue, int newValue)
    {
        TerrainDataModel.TerrainData[Id].HealPercent = newValue;
    }
    [ObservableProperty]
    private int[,] _movementCost;
    partial void OnMovementCostChanged(int[,]? oldValue, int[,] newValue)
    {
        TerrainDataModel.TerrainData[Id].MovementCost = newValue;
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
            MovementCost = MovementCost,
        };
    }
    public void Dispose()
    {
        TerrainDataModel.TerrainData.Remove(Id);
    }
}
