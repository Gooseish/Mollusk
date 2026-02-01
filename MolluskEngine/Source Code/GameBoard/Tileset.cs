using MolluskEngine.Data;

namespace MolluskEngine.GameBoard;

public class Tileset : IDataType
{
    public int Id {get;set;}
    /// <summary>
    /// String that corresponds to the image name of the tilemap
    /// </summary>
    public string Name {get;set;}
    public int[] TerrainData {get;set;}
    public Tileset()
    {
        Id = -1;
        Name = "New Tileset";
        TerrainData = new int[1]; // Todo: fix?
    }
}
