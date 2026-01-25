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
    public static IDataViewModel New(EditorName type)
    {
        return type switch
        {
            EditorName.Terrain => new TerrainDataViewModel(),
            _ => throw new Exception("Data view model could not resolve editor type"),
        };
    }
    public static ObservableCollection<IDataViewModel> GetCollection(EditorName type)
    {
        ObservableCollection<IDataViewModel> data = [];
        switch (type)
        {
            case EditorName.Terrain:
                foreach (Terrain terrain in TerrainDataModel.TerrainData.Values)
                    data.Add(new TerrainDataViewModel(terrain));
                break;
            default:
                throw new Exception("Data view model could not resolve editor type");
        }
        return data;
    }
}
