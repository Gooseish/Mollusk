using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Data;

namespace MolluskEditor.ViewModels;

/// <summary>
/// View model that handles a sidebar which allows an
/// editor to select a specific data item. For example,
/// in the terrain editor, an instance of this class is
/// present to allow the user to select which kind
/// of terrain they want to edit.
/// </summary>
public partial class DataSelectorSidebarViewModel<T> : ViewModelBase
    where T : IDataViewModel<T>, new()
{
    [ObservableProperty]
    private ObservableCollection<T> _data;
    [ObservableProperty]
    private int? _selectedDataIndex;
    [ObservableProperty]
    private T? _selectedData;

    public DataSelectorSidebarViewModel()
    {
        Initialize();
    }

    public void Initialize()
    {
        Data = T.ReadExisting(); // Should be using a factory here probably
        if (Data.Count > 0)
        {
            SelectedDataIndex = 0;
        }
    }
    #region Events
    partial void OnSelectedDataChanged(T? oldValue, T? newValue)
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
        Data.Add(new T()); // Should be using a factory here probably
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
    /// <summary>
    /// Keep collection items in order of ascending ID.
    /// </summary>
    private void SortData()
    {
        Data = new ObservableCollection<T>(Data.OrderBy(i => i.Id));
    }
    #endregion
}

public class TerrainSelectorViewModel : DataSelectorSidebarViewModel<TerrainDataViewModel> { }