using System.Collections.ObjectModel;

namespace MolluskEditor.Data;

public interface IDataViewModel<T> 
    where T : IDataViewModel<T>
{
    /// <summary>
    /// When a DataViewModel is removed, it should remove
    /// the corresponding item in the DataModel as well.
    /// </summary>
    public void Dispose();
    public int Id {get;set;}
    public static abstract ObservableCollection<T> ReadExisting(); // Todo: Can I make this non-abstract?
    
}
