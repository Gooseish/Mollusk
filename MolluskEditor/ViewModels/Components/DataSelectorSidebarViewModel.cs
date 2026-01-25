using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Data;

namespace MolluskEditor.ViewModels;

public partial class DataSelectorSidebarViewModel : ViewModelBase
{
    private Type _type;
    [ObservableProperty]
    private ObservableCollection<IDataViewModel> _data;
    [ObservableProperty]
    private int? _selectedDataIndex;
    [ObservableProperty]
    private IDataViewModel? _selectedData;

    public DataSelectorSidebarViewModel(Type type)
    {
        _type = type;
        Initialize();
    }

    public void Initialize()
    {
        Data = IDataViewModel.GetCollection(_type); // Should be using a factory here probably
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
        Data.Add(IDataViewModel.New(_type)); // Should be using a factory here probably
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
