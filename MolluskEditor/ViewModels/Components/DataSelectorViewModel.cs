using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MolluskEditor.ViewModels;

/// <summary>
/// View model that handles a sidebar which allows an
/// editor to select a specific data item. For example,
/// in the terrain editor, an instance of this class is
/// present to allow the user to select which kind
/// of terrain they want to edit.
/// </summary>
public partial class DataSelectorViewModel : ViewModelBase
{
    /// This Type object and getter function are a poor man's method
    /// of making this behave like a generic class, since xaml
    /// doesn't play nicely with generics.
    private Type _dataViewModelType;
    private Func<ObservableCollection<IDataViewModel>> _getFromDataModel;
    [ObservableProperty]
    private ObservableCollection<IDataViewModel> _data;
    [ObservableProperty]
    private int? _selectedDataIndex;
    [ObservableProperty]
    private IDataViewModel? _selectedData;

    public DataSelectorViewModel(Type dataViewModelType,
        Func<ObservableCollection<IDataViewModel>> reader)
    {
        _dataViewModelType = dataViewModelType;
        _getFromDataModel = reader;
        Initialize();
    }

    public void Initialize()
    {
        Data = _getFromDataModel.Invoke();
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
    /// <summary>
    /// Event that notifies the parent editor that the selected data
    /// index has changed.
    /// </summary>
    public event EventHandler IndexChanged;
    #endregion

    #region Relay Commands
    [RelayCommand]
    private void AddData()
    {
        Data.Add((IDataViewModel)Activator.CreateInstance(_dataViewModelType));
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
    /// <summary>
    /// Pick a reasonable valid selected item index
    /// when the number of items in the collection
    /// changes.
    /// </summary>
    /// <param name="lastIndex"></param>
    public void FixIndex(int? lastIndex)
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
    /// <summary>
    /// Keep collection items in order of ascending ID.
    /// </summary>
    private void SortData()
    {
        Data = new ObservableCollection<IDataViewModel>(Data.OrderBy(i => i.Id));
    }
    #endregion
}