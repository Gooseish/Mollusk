

namespace MolluskEngine.Data;

public interface IDataType
{
    // All classes which implement IDataType should
    // implement new()
    public int Id {get;set;}
    public string Name {get;set;}
}
