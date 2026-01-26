using MolluskEngine.Data;

namespace MolluskEngine.GameBoard;

public class Terrain : IDataType
{
    public int Id {get;set;}
    public string Name {get;set;}
    public int Avoid {get;set;}
    public int Def {get;set;}
    public int Res {get;set;}
    public bool Heals {get;set;} // Should probably be a property that checks if HealPercent > 0
    public int HealPercent {get;set;}
    /// <summary>
    /// Table of movement costs, where the first index is 
    /// the weather and the second index is the movement 
    /// type of the unit
    /// </summary>
    public int[,] MovementCost {get;set;}
}
