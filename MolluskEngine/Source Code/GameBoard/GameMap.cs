using System;
using System.Collections.Generic;
using MolluskEngine.Data;

namespace MolluskEngine.GameBoard;

public class GameMap : IDataType
{
    public int Id {get; set;}
    public string Name {get; set;}
    public int Tileset {get; set;}
    public int Height {get; set;}
    public int Width {get; set;}

    /// <summary>
    /// The int for each element of tileData corresponds to a specific tile 
    /// on the tileset. The position of each element in the array corresponds
    /// to the map coordinates.
    /// </summary>
    public int[,] TileData {get; set;}
    public GameMap()
    {
        Id = -1;
        Name = "New Map";
        Tileset = -1;
        Height = 10;
        Width = 15;
        TileData = new int[Width, Height];
    }
}