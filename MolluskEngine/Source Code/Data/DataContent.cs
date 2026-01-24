using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using MolluskEngine.GameBoard;

namespace MolluskEngine.Data;
/// <summary>
/// Handles loading of json-based content.
/// </summary>
public static class DataContent
{
    private static Dictionary<int, Terrain> terrainData;
    public static IReadOnlyDictionary<int, Terrain> TerrainData {get{return terrainData;}}
    public static void LoadContent(ContentManager content)
    {
        terrainData = content.Load<Dictionary<int, Terrain>>("Data/TerrainData");
    }
}
