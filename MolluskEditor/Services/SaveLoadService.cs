using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using JsonPipeline;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Services;

public static class SaveLoadService
{
    public static string? ContentRoot = @"C:/Users/Home/Documents/Monogame Projects/Mollusk/MolluskEngine/Content/"; // Shouldn't be hardcoded
    public static readonly string DATAPATH = @"Data/";
    public static readonly string TERRAINPATH = "TerrainData.json";
    public static readonly string TILESETPATH = "TilesetData.json";
    public static void Save()
    {
        if (ContentRoot == null)
            return; // Prompt to pick new folder here
    
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = {new Array2DConverter()},
        };
        // Terrain
        string jsonString = JsonSerializer.Serialize(TerrainDataModel.TerrainData, options);
        File.WriteAllText(ContentRoot + DATAPATH + TERRAINPATH, jsonString);
        // Tilesets
        jsonString = JsonSerializer.Serialize(TilesetDataModel.TilesetData, options);
        File.WriteAllText(ContentRoot + DATAPATH + TILESETPATH, jsonString);
    }
    public static void Open()
    {
        var options = new JsonSerializerOptions
        {
            Converters = {new Array2DConverter()},
        };

        // Terrain
        string jsonString = File.ReadAllText(ContentRoot + DATAPATH + TERRAINPATH);
        Dictionary<int, Terrain>? TerrainData = JsonSerializer.Deserialize<Dictionary<int, Terrain>>(jsonString, options);
        if (TerrainData != null)
            TerrainDataModel.TerrainData = TerrainData;
        // Tilesets
        /*
        TilesetDataModel.TilesetData = ReadEntry<Dictionary<int, Tileset>>(
            ContentRoot + DATAPATH + TILESETPATH, options);
            */

        OnProjectLoaded();
    }
    private static T? ReadEntry<T>(string path, JsonSerializerOptions options)
    {
        T? data = default;
        try
        {
            string jsonString = File.ReadAllText(path);
            data = JsonSerializer.Deserialize<T>(jsonString, options);
        }
        catch {}
        
        return data;
    }
    private static void OnProjectLoaded()
    {
        if (ProjectLoaded == null)
            return;
        ProjectLoaded.Invoke(null, EventArgs.Empty);
    }
    public static event EventHandler ProjectLoaded;
}
