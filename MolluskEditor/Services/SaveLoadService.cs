using System;
using System.IO;
using System.Text.Json;
using MolluskEditor.Models;

namespace MolluskEditor.Services;

public static class SaveLoadService
{
    public static string? ContentRoot = @"../../MolluskEngine/Content/Assets/"; // Shouldn't be hardcoded
    public static string TerrainPath = "Data/Terrain/";
    public static void Save()
    {
        if (ContentRoot == null)
            return; // Prompt to pick new folder here
    
        string jsonString = JsonSerializer.Serialize(TerrainDataModel.TerrainData);
        File.WriteAllText(ContentRoot + TerrainPath + "terrainData.json", jsonString);
    }
    public static void Open()
    {
        
    }
}
