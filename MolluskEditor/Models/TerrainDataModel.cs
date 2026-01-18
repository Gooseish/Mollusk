using System;
using System.Collections.Generic;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Models;

public static class TerrainDataModel
{
    public static Dictionary<int, Terrain> TerrainData = new();
    public static Terrain newTerrain()
    {
        Terrain result = new Terrain()
        {
            Id = NextTerrainId(),
            Name = "New Terrain",
            Avoid = 0,
            Def = 0,
            Res = 0,
            Heals = false,
            HealPercent = 0,
            MovementCost = DefaultMoveCost(),
        };
        TerrainData[result.Id] = result;
        return result;
    }
    private static int NextTerrainId()
    {
        int n = 0;
        while (TerrainData.Keys.Contains(n))
            n++;
        return n;
    }
    private static Dictionary<MovementType, int> DefaultMoveCost()
    {
        Dictionary<MovementType, int> result = new();
        foreach(MovementType movementType in Enum.GetValues(typeof(MovementType)))
            result[movementType] = 1;
        return result;
    }
}

