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
    public static IDataViewModel New(Type type)
    {
        return new TerrainDataViewModel();
    }
    public static ObservableCollection<IDataViewModel> GetCollection(Type type)
    {
        ObservableCollection<IDataViewModel> data = [];

        foreach (Terrain terrain in TerrainDataModel.TerrainData.Values)
            data.Add(new TerrainDataViewModel(terrain));

        return data;
    }
}
