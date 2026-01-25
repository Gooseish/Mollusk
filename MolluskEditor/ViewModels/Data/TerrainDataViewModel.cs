using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Data;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;
using MolluskEngine.Extensions;

namespace MolluskEditor.ViewModels;

public partial class TerrainDataViewModel : ObservableObject, IDataViewModel
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
        _movementCost = GetMovementCostCollection(terrain.MovementCost);
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
        _movementCost = GetMovementCostCollection(terrain.MovementCost);
    }
    #region Boilerplate Properties
    private Terrain _terrain {get {return TerrainDataModel.TerrainData[Id];}}
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
    private ObservableCollection<int> _movementCost;
    partial void OnMovementCostChanged(
        ObservableCollection<int>? oldValue,
        ObservableCollection<int> newValue)
    {
        _terrain.MovementCost = GetMovementCostArray(newValue);
    }
    private int[,] GetMovementCostArray(ObservableCollection<int> movementCostCollection)
    {
        // Todo: Make this a generic extension
        int movementTypeCount = MovementType.Count();
        int weatherTypeCount = WeatherType.Count();
        int[,] result = new int[weatherTypeCount,movementTypeCount];
        int n = 0;
        for (int m = 0; m < weatherTypeCount; m++)
            for (int l = 0; l < movementTypeCount; l++)
            {
                result[m,l] = movementCostCollection[n];
                n++;
            }
        return result;
    }
    private ObservableCollection<int> GetMovementCostCollection(int[,] movementCostArray)
    {
        // Todo: Make this a generic extension
        int movementTypeCount = MovementType.Count();
        int weatherTypeCount = WeatherType.Count();
        ObservableCollection<int> result = new();
        for (int m = 0; m < weatherTypeCount; m++)
            for (int l = 0; l < movementTypeCount; l++)
                result.Add(movementCostArray[m, l]);
        return result;
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
        TerrainDataModel.TerrainData.Remove(Id);
    }
}
