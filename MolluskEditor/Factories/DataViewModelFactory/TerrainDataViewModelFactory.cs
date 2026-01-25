using System;
using System.Collections.ObjectModel;
using MolluskEditor.Data;
using MolluskEditor.Models;
using MolluskEditor.ViewModels;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Factories.DataViewModelFactory;

public class TerrainDataViewModelFactory : IDataViewModelFactory
{
    public IDataViewModel New()
    {
        return new TerrainDataViewModel();
    }
    public ObservableCollection<IDataViewModel> ReadExisting()
    {
        ObservableCollection<IDataViewModel> data = [];
        foreach (Terrain terrain in TerrainDataModel.TerrainData.Values)
            data.Add(new TerrainDataViewModel(terrain));
        return data;
    }
}
