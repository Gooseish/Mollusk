using System;
using System.Text.Json;
using JsonPipeline;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Services;

public class SaveLoadService
{
    public static readonly string? CONTENTROOT = @"C:/Users/Home/Documents/Monogame_Projects/Mollusk/MolluskEngine/Content/"; // Shouldn't be hardcoded
    private static readonly string DATAPATH = @"Data/";
    public static readonly string TILESETIMAGES = @"Graphics/Tilesets/";
    private static readonly string TERRAINPATH = "TerrainData.json";
    private static readonly string TILESETPATH = "TilesetData.json";
    private static readonly string MAPPATH = "MapData.json";
    private DataModel<Terrain> _terrainData;
    private DataModel<Tileset> _tilesetData;
    private DataModel<GameMap> _mapData;
    public SaveLoadService(DataModel<Terrain> terrainData, DataModel<Tileset> tilesetData,
        DataModel<GameMap> mapData)
    {
        _terrainData = terrainData;
        _tilesetData = tilesetData;
        _mapData = mapData;
    }
    public void Save()
    {
        if (CONTENTROOT == null)
            return; // Prompt to pick new folder here
    
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = {new Array2DConverter(), new ValueTupleConverter<int, int>()},
        };
        
        _terrainData.Write(CONTENTROOT + DATAPATH + TERRAINPATH, options);
        _tilesetData.Write(CONTENTROOT + DATAPATH + TILESETPATH, options);
        _mapData.Write(CONTENTROOT + DATAPATH + MAPPATH, options);
    }
    public void Open()
    {
        var options = new JsonSerializerOptions
        {
            Converters = {new Array2DConverter(), new ValueTupleConverter<int, int>()},
        };
        
        _terrainData.Read(CONTENTROOT + DATAPATH + TERRAINPATH, options);
        _tilesetData.Read(CONTENTROOT + DATAPATH + TILESETPATH, options);
        _mapData.Read(CONTENTROOT + DATAPATH + MAPPATH, options);
        
        OnProjectLoaded();
    }
    private static void OnProjectLoaded()
    {
        if (ProjectLoaded == null)
            return;
        ProjectLoaded.Invoke(null, EventArgs.Empty);
    }
    public static event EventHandler? ProjectLoaded;
}