using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class DataSelectorSidebarViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<TerrainDataViewModel> _Data;
    [ObservableProperty]
    private int? _selectedDataIndex;
    [ObservableProperty]
    private TerrainDataViewModel? _selectedData;

    private void Initialize()
    {
        ObservableCollection<TerrainDataViewModel> data = [];
        getTerrainData();
        void getTerrainData()
        {
            foreach (Terrain terrain in TerrainDataModel.TerrainData.Values)
                data.Add(new TerrainDataViewModel(terrain));
        }
        Data = data;
        if (Data.Count > 0)
        {
            SelectedDataIndex = 0;
        }
    }

    #region Relay Commands
    [RelayCommand]
    private void AddData()
    {
        Data.Add(new TerrainDataViewModel());
        SelectedDataIndex = Data.Count - 1;
        SortTerrain();
    }
    [RelayCommand]
    private void RemoveData()
    {
        if (SelectedData == null)
            return;
        int? lastIndex = SelectedDataIndex;
        SelectedData.Dispose();
        Data.Remove(SelectedData);
        FixIndex(lastIndex);
    }
    #endregion

    #region Private Utilities
    private void FixIndex(int? lastIndex)
    {
        try
        {
            _ = Data[(int)lastIndex - 1];
            SelectedDataIndex = lastIndex - 1;
            return;
        }
        catch
        {
            if (Data.Count > 0)
                SelectedDataIndex = 0;
        }
    }
    private void SortTerrain()
    {
        Data = new ObservableCollection<TerrainDataViewModel>(Data.OrderBy(i => i.Id));
    }
    #endregion
}
