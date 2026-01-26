

namespace MolluskEngine.GameBoard;

public class Tileset
{
    public int Id;
    public string Name;
    /// <summary>
    /// String that corresponds to the image name of the tilemap
    /// </summary>
    public string ImageData;
    /// <summary>
    /// Ids of all terrain 
    /// </summary>
    public int[] TerrainData;
}
