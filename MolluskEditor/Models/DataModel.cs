using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MolluskEngine.Data;

namespace MolluskEditor.Models;

public class DataModel<T> where T : IDataType, new()
{
    public Dictionary<int, T> Data = []; // Public face should be IReadOnlyDictionary?
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
    public bool CheckIdAvailable(string idString, int inquirerId) // This should be part of the DataModel class?
    {
        if (!int.TryParse(idString, out int id)) // Don't reject because ParseAsInt should reject it
            return true;
        if (id == inquirerId) // Id is "available" because it's already yours
            return true;
        return !Data.ContainsKey(id);
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
    # region Events
    public void OnIdsChanged()
    {
        if (IdsChanged == null)
            return;
        IdsChanged.Invoke(null, EventArgs.Empty);
    }
    public EventHandler? IdsChanged;

    public void OnAnyChange()
    {
        if (AnyChange == null)
            return;
        AnyChange.Invoke(null, EventArgs.Empty);
    }
    public EventHandler? AnyChange;
    #endregion
}
