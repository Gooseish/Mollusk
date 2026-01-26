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
}