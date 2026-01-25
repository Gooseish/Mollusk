using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Data;
using MolluskEditor.Factories;

namespace MolluskEditor.ViewModels;

public partial class DataSelectorSidebarViewModel : ViewModelBase
{
    private IDataViewModelFactory _factory;
    [ObservableProperty]
    private ObservableCollection<IDataViewModel> _data;
    [ObservableProperty]
    private int? _selectedDataIndex;
    [ObservableProperty]
    private IDataViewModel? _selectedData;

    public DataSelectorSidebarViewModel(IDataViewModelFactory factory)
    {
        _factory = factory;
        Initialize();
    }

    public void Initialize()
    {
        Data = _factory.ReadExisting(); // Should be using a factory here probably
        if (Data.Count > 0)
        {
            SelectedDataIndex = 0;
        }
    }
    #region Events
    partial void OnSelectedDataChanged(IDataViewModel? oldValue, IDataViewModel? newValue)
    {
        IndexChanged.Invoke(this, EventArgs.Empty);
    }
    public event EventHandler IndexChanged;
    #endregion

    #region Relay Commands
    [RelayCommand]
    private void AddData()
    {
        Data.Add(_factory.New()); // Should be using a factory here probably
        SelectedDataIndex = Data.Count - 1;
        SortData();
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
    private void SortData()
    {
        Data = new ObservableCollection<IDataViewModel>(Data.OrderBy(i => i.Id));
    }
    #endregion
}
