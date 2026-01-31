using MolluskEngine.Data;

namespace MolluskEngine.GameBoard;

public class Tileset : IDataType
{
    public int Id {get;set;}
    public string Name {get;set;}
    /// <summary>
    /// String that corresponds to the image name of the tilemap
    /// </summary>
    public string ImageData {get;set;}
    /// <summary>
    /// Ids of all terrain 
    /// </summary>
    public int[] TerrainData {get;set;}
    public Tileset()
    {
        Id = -1;
        Name = "New Tileset";
        ImageData = "";
        TerrainData = new int[1]; // Todo: fix?
    }
}
