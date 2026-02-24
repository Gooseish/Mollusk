using System;
using System.Collections.Generic;
using MolluskEngine.Data;

namespace MolluskEngine.GameBoard;

public class GameMap : IDataType
{
    public int Id {get; set;}
    public string Name {get; set;}
    public int Height {get; set;}
    public int Width {get; set;}

    /// <summary>
    /// The int for each element of tileData corresponds to a specific tile 
    /// on the tileset. The position of each element in the array corresponds
    /// to the map coordinates. The first element of the value tuple is the
    /// tileset id and the second element is the id of the specific tile in
    /// the tileset.
    /// </summary>
    public Tile[,] TileData {get; set;}
    public GameMap ResizeMap(int x, int y)
    {
        GameMap result = new GameMap();
        return result;
    }
    public GameMap()
    {
        Id = -1;
        Name = "New Map";
        Height = 10;
        Width = 15;
        TileData = new Tile[Width, Height];
    }
}