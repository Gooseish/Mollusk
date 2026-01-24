using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Services;

public static class SaveLoadService
{
    public static string? ContentRoot = @"C:/Users/Home/Documents/Monogame Projects/Mollusk/MolluskEngine/Content/"; // Shouldn't be hardcoded
    public static string TerrainPath = @"Data/";
    public static void Save()
    {
        if (ContentRoot == null)
            return; // Prompt to pick new folder here
    
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string jsonString = JsonSerializer.Serialize(TerrainDataModel.TerrainData, options);
        File.WriteAllText(ContentRoot + TerrainPath + "TerrainData.json", jsonString);
    }
    public static void Open()
    {
        string jsonString = File.ReadAllText(ContentRoot + TerrainPath + "TerrainData.json");
        Dictionary<int, Terrain>? TerrainData = JsonSerializer.Deserialize<Dictionary<int, Terrain>>(jsonString);
        if (TerrainData != null)
            TerrainDataModel.TerrainData = TerrainData;

        OnProjectLoaded();
    }
    private static void OnProjectLoaded()
    {
        ProjectLoaded.Invoke(null, EventArgs.Empty);
    }
    public static event EventHandler ProjectLoaded;
}
