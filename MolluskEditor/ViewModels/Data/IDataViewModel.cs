using System;
using System.Collections.ObjectModel;
using MolluskEditor.Models;
using MolluskEditor.ViewModels;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Data;

public interface IDataViewModel
{
    public void Dispose();
    public int Id {get;set;}
}
