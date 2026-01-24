using System;
using System.Collections.Generic;
using System.Linq;
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
    private static int[,] DefaultMoveCost()
    {
        int[,] result = new int[Enum.GetNames(typeof(WeatherType)).Count(),
                                Enum.GetNames(typeof(MovementType)).Count()];
        foreach(WeatherType weatherType in Enum.GetValues(typeof(WeatherType)))
            foreach(MovementType movementType in Enum.GetValues(typeof(MovementType)))
                result[(int)weatherType, (int)movementType] = 1;
        return result;
    }
}

