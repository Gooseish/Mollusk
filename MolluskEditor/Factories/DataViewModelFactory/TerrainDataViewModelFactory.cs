using System;
using System.Collections.ObjectModel;
using MolluskEditor.Data;
using MolluskEditor.Models;
using MolluskEditor.ViewModels;
using MolluskEngine.GameBoard;

namespace MolluskEditor.Factories.DataViewModelFactory;

public class TerrainDataViewModelFactory : IDataViewModelFactory
{
    private TerrainDataModel _terrainData;
    public TerrainDataViewModelFactory(TerrainDataModel terrainData)
    {
        _terrainData = terrainData;
    }
    public IDataViewModel New()
    {
        return new TerrainDataViewModel(_terrainData);
    }
    public ObservableCollection<IDataViewModel> ReadExisting()
    {
        ObservableCollection<IDataViewModel> data = [];
        foreach (Terrain terrain in _terrainData.TerrainData.Values)
            data.Add(new TerrainDataViewModel(_terrainData, terrain));
        return data;
    }
}
