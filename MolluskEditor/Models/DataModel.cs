using System.Collections.Generic;
using MolluskEngine.Data;

namespace MolluskEditor.Models;

public class DataModel<T> where T : IDataType, new()
{
    public Dictionary<int, T> Data = [];
    public T New()
    {
        T result = new();
        result.Id = NextId();
        return result;
    }
    private int NextId()
    {
        int n = 0;
        while (Data.ContainsKey(n))
            n++;
        return n;
    }
}
