using System;
using System.Collections.ObjectModel;
using MolluskEditor.Models;
using MolluskEditor.ViewModels;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Data;

public interface IDataViewModel
{
    /// <summary>
    /// When a DataViewModel is removed, it should remove
    /// the corresponding item in the DataModel as well.
    /// </summary>
    public void Dispose();
    public int Id {get;set;}
}
