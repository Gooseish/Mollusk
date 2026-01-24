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
    public static void Save()
    {
        if (ContentRoot == null)
            return; // Prompt to pick new folder here
    
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = {new Array2DConverter()},
        };
        string jsonString = JsonSerializer.Serialize(TerrainDataModel.TerrainData, options);
        File.WriteAllText(ContentRoot + DATAPATH + TERRAINPATH, jsonString);
    }
    public static void Open()
    {
        string jsonString = File.ReadAllText(ContentRoot + DATAPATH + TERRAINPATH);
        Dictionary<int, Terrain>? TerrainData = JsonSerializer.Deserialize<Dictionary<int, Terrain>>(jsonString);
        if (TerrainData != null)
            TerrainDataModel.TerrainData = TerrainData;

        OnProjectLoaded();
    }
    private static void OnProjectLoaded()
    {
        if (ProjectLoaded == null)
            return;
        ProjectLoaded.Invoke(null, EventArgs.Empty);
    }
    public static event EventHandler ProjectLoaded;
}
