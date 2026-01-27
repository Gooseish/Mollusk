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
    private static Dictionary<int, Tileset> tilesetData;
    public static IReadOnlyDictionary<int, Tileset> TilesetData;
    public static void LoadContent(ContentManager content)
    {
        terrainData = content.Load<Dictionary<int, Terrain>>("Data/TerrainData");
        //tilesetData = content.Load<Dictionary<int, Tileset>>("Data/TilesetData");
    }
}
