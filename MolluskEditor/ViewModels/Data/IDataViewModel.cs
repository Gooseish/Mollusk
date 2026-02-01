

using System.Collections.ObjectModel;
using Avalonia.Media;

namespace MolluskEditor.ViewModels;

public interface IDataViewModel
{
    /// <summary>
    /// When a DataViewModel is removed, it should remove
    /// the corresponding item in the DataModel as well.
    /// </summary>
    public void Unregister();
    public void Register();
    public string Id {get;set;}
    public string Name {get;set;}
    public bool CheckIdAvailable(string idString);
    public void FixFields();
    public void NotifyChange();
    public IBrush TextColor {get;set;}
}
