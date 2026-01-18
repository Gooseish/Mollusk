using System;
using System.IO;
using System.Text.Json;
using MolluskEditor.Models;

namespace MolluskEditor.Services;

public static class SaveLoadService
{
    public static string? ContentRoot = @"C:/Users/Home/Documents/Monogame Projects/Mollusk/MolluskEngine/Content/"; // Shouldn't be hardcoded
    public static string TerrainPath = @"Data/";
    public static void Save()
    {
        if (ContentRoot == null)
            return; // Prompt to pick new folder here
    
        string jsonString = JsonSerializer.Serialize(TerrainDataModel.TerrainData);
        File.WriteAllText(ContentRoot + TerrainPath + "TerrainData.json", jsonString);
    }
    public static void Open()
    {
        
    }
}
