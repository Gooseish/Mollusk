using System.Collections.Generic;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Models;

public class TilesetDataModel
{
    public Dictionary<int, Tileset> TilesetData = [];
    public Tileset NewTileset()
    {
        Tileset result = new()
        {
            Id = NextTilesetId(),
            Name = "New Tileset",
            ImageData = "",
            TerrainData = new int[1], // Todo: fix?
        };
        TilesetData[result.Id] = result;
        return result;
    }
    private int NextTilesetId()
    {
        int n = 0;
        while (TilesetData.ContainsKey(n))
            n++;
        return n;
    }
}
