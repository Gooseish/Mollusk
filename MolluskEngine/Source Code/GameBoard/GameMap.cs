using System;
using System.Collections.Generic;

namespace MolluskEngine.GameBoard;

public class GameMap
{
    public int Id;
    public string Name;
    public int Tileset;
    public int Height;
    public int Width;

    /// <summary>
    /// The int for each element of tileData corresponds to a specific tile 
    /// on the tileset. The position of each element in the array corresponds
    /// to the map coordinates.
    /// </summary>
    public int[,] TileData;
}