using System;

namespace MolluskEngine.GameBoard;

public struct Tile : IEquatable<Tile>
{
    public int TilesetId {get;set;}
    public int TileId {get;set;}
    public Tile()
    {
        TilesetId = 0;
        TileId = 0;
    }
    public Tile(int tilesetId, int tileId)
    {
        TilesetId = tilesetId;
        TileId = tileId;
    }
    public bool Equals(Tile other)
    {
        return TilesetId == other.TilesetId && TileId == other.TileId;
    }
}
