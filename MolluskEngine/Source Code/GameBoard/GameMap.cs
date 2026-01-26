using System;
using System.Collections.Generic;

namespace MolluskEngine.GameBoard;

public class GameMap
{
    public string Tileset; // Int?
    public int Height;
    public int Width;
    /// <summary>
    /// Terrain map of the tileset. Keys represent 
    /// tile ids, values represent terrain type id.
    /// </summary>
    public Dictionary<int, int> TerrainMap;
    /// <summary>
    /// The int for each element of tileData corresponds to a specific tile 
    /// on the tileset. The position of each element in the array corresponds
    /// to the map coordinates.
    /// </summary>
    public int[,] TileData;
}