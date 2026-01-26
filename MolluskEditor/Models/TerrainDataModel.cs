using System;
using System.Collections.Generic;
using System.Linq;
using MolluskEngine.Data;
using MolluskEngine.Extensions;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Models;

public class TerrainDataModel
{
    public Dictionary<int, Terrain> TerrainData = [];
    public Terrain NewTerrain()
    {
        Terrain result = new()
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
    private int NextTerrainId()
    {
        int n = 0;
        while (TerrainData.ContainsKey(n))
            n++;
        return n;
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