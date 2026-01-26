using System.Collections.Generic;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Models;

public static class TilesetDataModel
{
    public static Dictionary<int, Tileset> TilesetData = [];
    public static Tileset NewTileset()
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
    private static int NextTilesetId()
    {
        int n = 0;
        while (TilesetData.ContainsKey(n))
            n++;
        return n;
    }
}
