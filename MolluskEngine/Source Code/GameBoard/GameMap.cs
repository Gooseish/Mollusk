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
    public GameMap ResizeMap(int width, int height)
    {
        GameMap result = new GameMap()
        {
            Id = Id,
            Name = Name,
            Height = height,
            Width = width,
            TileData = new Tile[width, height],
        };
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                if (result.Contains(x, y))
                    result.TileData[x, y] = TileData[x, y];
        return result;
    }
    public bool Contains(int x, int y)
    {
        try
        {
            _ = TileData[x, y];
            return true;
        }
        catch {return false;}
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