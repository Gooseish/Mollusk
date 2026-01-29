using System;
using MolluskEngine.Data;
using MolluskEngine.Extensions;

namespace MolluskEngine.GameBoard;

public class Terrain : IDataType
{
    public int Id {get;set;}
    public string Name {get;set;}
    public int Avoid {get;set;}
    public int Def {get;set;}
    public int Res {get;set;}
    public bool Heals {get {return HealPercent > 0;}} // Should probably be a property that checks if HealPercent > 0
    public int HealPercent {get;set;}
    /// <summary>
    /// Table of movement costs, where the first index is 
    /// the weather and the second index is the movement 
    /// type of the unit
    /// </summary>
    public int[,] MovementCost {get;set;}
    public Terrain()
    {
        Id = -1;
        Name = "New Terrain";
        Avoid = 0;
        Def = 0;
        Res = 0;
        HealPercent = 0;
        MovementCost = DefaultMoveCost();
    }
    private static int[,] DefaultMoveCost()
    {
        int[,] result = new int[WeatherType.Count(), MovementType.Count()];
        foreach(WeatherType weatherType in Enum.GetValues(typeof(WeatherType)))
            foreach(MovementType movementType in Enum.GetValues(typeof(MovementType)))
                result[(int)weatherType, (int)movementType] = 1;
        return result;
    }
}
