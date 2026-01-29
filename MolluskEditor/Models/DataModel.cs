using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MolluskEngine.Data;

namespace MolluskEditor.Models;

public class DataModel<T> where T : IDataType, new()
{
    public Dictionary<int, T> Data = [];
    public T New()
    {
        T result = new();
        result.Id = NextId();
        Data[result.Id] = result;
        return result;
    }
    public int NextId()
    {
        int n = 0;
        while (Data.ContainsKey(n))
            n++;
        return n;
    }
    public void Write(string path, JsonSerializerOptions options)
    {
        string jsonString = JsonSerializer.Serialize(Data, options);
        File.WriteAllText(path, jsonString);
    }
    public void Read(string path, JsonSerializerOptions options)
    {
        Data = [];
        try
        {
            string jsonString = File.ReadAllText(path);
            Data = JsonSerializer.Deserialize<Dictionary<int, T>>(jsonString, options) ?? [];
        }
        catch {} // Dangerous?
    }
}
