using System;
using System.Collections.Generic;

namespace MolluskEngine.GameBoard;

public class Terrain
{
    public int Id {get;set;}
    public string Name {get;set;}
    public int Avoid {get;set;}
    public int Def {get;set;}
    public int Res {get;set;}
    public bool Heals {get;set;} // Should probably be a property that checks if HealPercent > 0
    public int HealPercent {get;set;}
    public Dictionary<MovementType, int> MovementCost {get;set;} // Needs a way to account for weather
}
