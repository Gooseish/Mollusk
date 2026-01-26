using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using JsonPipeline;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Services;

public class SaveLoadService
{
    public static string? ContentRoot = @"C:/Users/Home/Documents/Monogame Projects/Mollusk/MolluskEngine/Content/"; // Shouldn't be hardcoded
    public static readonly string DATAPATH = @"Data/";
    public static readonly string TERRAINPATH = "TerrainData.json";
    public static readonly string TILESETPATH = "TilesetData.json";
    private DataModel<Terrain> _terrainData;
    private DataModel<Tileset> _tilesetData;
    public SaveLoadService(DataModel<Terrain> terrainData, DataModel<Tileset> tilesetData)
    {
        _terrainData = terrainData;
        _tilesetData = tilesetData;
    }
    public void Save()
    {
        if (ContentRoot == null)
            return; // Prompt to pick new folder here
    
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = {new Array2DConverter()},
        };
        
        // Terrain
        WriteEntry(_terrainData.Data, 
            ContentRoot + DATAPATH + TERRAINPATH, options);
        // Tilesets
        WriteEntry(_tilesetData.Data, 
            ContentRoot + DATAPATH + TILESETPATH, options);
        
    }
    public void Open()
    {
        var options = new JsonSerializerOptions
        {
            Converters = {new Array2DConverter()},
        };
        
        // Terrain
        _terrainData.Data = ReadEntry<Dictionary<int, Terrain>>(
            ContentRoot + DATAPATH + TERRAINPATH, options) ?? [];
        // Tilesets
        _tilesetData.Data = ReadEntry<Dictionary<int, Tileset>>(
            ContentRoot + DATAPATH + TILESETPATH, options) ?? [];
        
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
    private static void WriteEntry<T>(T obj, string path, JsonSerializerOptions options)
    {
        string jsonString = JsonSerializer.Serialize(obj, options);
        File.WriteAllText(path, jsonString);
    }
    private static void OnProjectLoaded()
    {
        if (ProjectLoaded == null)
            return;
        ProjectLoaded.Invoke(null, EventArgs.Empty);
    }
    public static event EventHandler? ProjectLoaded;
}
