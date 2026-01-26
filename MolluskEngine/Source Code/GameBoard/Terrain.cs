

namespace MolluskEngine.GameBoard;

public class Terrain
{
    public int Id;
    public string Name;
    public int Avoid;
    public int Def;
    public int Res;
    public bool Heals; // Should probably be a property that checks if HealPercent > 0
    public int HealPercent;
    /// <summary>
    /// Table of movement costs, where the first index is 
    /// the weather and the second index is the movement 
    /// type of the unit
    /// </summary>
    public int[,] MovementCost {get;set;}
}
