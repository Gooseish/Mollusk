using System;
using System.Collections.ObjectModel;
using MolluskEditor.Data;
using MolluskEditor.Models;
using MolluskEditor.ViewModels;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Factories.DataViewModelFactory;
/*
public class TerrainDataViewModelFactory : IDataViewModelFactory
{
    private DataModel<Terrain> _terrainData;
    public TerrainDataViewModelFactory(DataModel<Terrain> terrainData)
    {
        _terrainData = terrainData;
    }
    public IDataViewModel New()
    {
        return new TerrainDataViewModel();
    }
    public ObservableCollection<IDataViewModel> ReadExisting()
    {
        ObservableCollection<IDataViewModel> data = [];
        foreach (Terrain terrain in _terrainData.Data.Values)
            data.Add(new TerrainDataViewModel(terrain));
        return data;
    }
}
*/