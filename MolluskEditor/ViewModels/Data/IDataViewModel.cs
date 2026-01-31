

using System.Collections.ObjectModel;

namespace MolluskEditor.ViewModels;

public interface IDataViewModel
{
    /// <summary>
    /// When a DataViewModel is removed, it should remove
    /// the corresponding item in the DataModel as well.
    /// </summary>
    public void Dispose();
    public string Id {get;set;}
    public string Name {get;set;}
    public bool CheckIdAvailable(string idString);
    public void FixFields();
}
