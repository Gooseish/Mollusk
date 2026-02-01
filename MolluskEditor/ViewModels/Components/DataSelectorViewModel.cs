using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Commands;

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
    /// <summary>
    /// Some data selectors are not intended to be used as editors and are readonly,
    /// so have this value as false.
    /// </summary>
    [ObservableProperty]
    private bool _writeable;
    /// <summary>
    /// Function passed in with dependency injection that is responsible
    /// for looking at the data model and getting a list of matching
    /// view models.
    /// </summary>
    private Func<ObservableCollection<IDataViewModel>> _getFromDataModel;
    /// <summary>
    /// Collection of all the data models.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<IDataViewModel> _data = [];
    [ObservableProperty]
    private int? _selectedDataIndex;
    [ObservableProperty]
    private IDataViewModel? _selectedData;
    private CommandStack _commandStack;

    // Constructor's getting unwieldy, maybe build with factory model and DI?
    public DataSelectorViewModel(Type dataViewModelType,
        Func<ObservableCollection<IDataViewModel>> reader, CommandStack commandStack,
        bool writeable = true)
    {
        _dataViewModelType = dataViewModelType;
        _getFromDataModel = reader;
        _commandStack = commandStack;
        _writeable = writeable;
        Initialize();
        SearchText = "";
    }

    public void Initialize()
    {
        Data = _getFromDataModel.Invoke();
        SortData();
        if (SearchFilteredData.Count() == 0)
            SearchFilteredData = Data;
        if (Data.Count > 0) { SelectedDataIndex = 0; }
    }
    #region Search Box
    [ObservableProperty]
    private string _searchText;
    [ObservableProperty]
    private ObservableCollection<IDataViewModel> _searchFilteredData = [];
    partial void OnSearchTextChanged(string? oldValue, string newValue)
    {
        DoSearch(newValue);
    }
    [RelayCommand]
    private void SnapToSearchResult()
    {
        if (SearchFilteredData.Count() > 0)
            SelectedData = SearchFilteredData[0];
        SearchText = "";
    }
    private void DoSearch(string searchText) // Make this an async task
    {
        if (string.IsNullOrEmpty(searchText))
        {
            SearchFilteredData = Data;
            return;
        }
        ObservableCollection<IDataViewModel> result = [];
        foreach (IDataViewModel viewModel in Data)
            if (viewModel.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                result.Add(viewModel);
        SearchFilteredData = result;
    }
    #endregion
    #region Events
    partial void OnSelectedDataChanged(IDataViewModel? oldValue, IDataViewModel? newValue)
    {
        if (IndexChanged == null) {return;}
        IndexChanged.Invoke(this, EventArgs.Empty);
        if (SelectedData != null)
            SelectedData.FixFields();
    }
    /// <summary>
    /// Event that notifies the parent editor that the selected data
    /// index has changed.
    /// </summary>
    public event EventHandler? IndexChanged;
    public void SortDataEvent(object? sender, EventArgs args)
    {
        SortData();
    }
    #endregion

    #region Relay Commands
    [RelayCommand]
    private void AddData()
    {
        var newElement = (IDataViewModel)Activator.CreateInstance(_dataViewModelType);
        if (newElement == null) { return; }
        // Issue Command
        CommandSequence command = new();
        command.Add(new CustomCommand(newElement.Register, newElement.Unregister)); // Register to the datamodel
        command.Add(new AddToCollectionCommand<IDataViewModel>(Data, newElement)); // Add to the collection
        command.AddCleanup(newElement.NotifyChange); // Notify any readonly selectors that might be listening
        _commandStack.IssueCommand(command);
        // Cleanup
        SelectedDataIndex = Data.Count - 1; // Fix Index
    }
    [RelayCommand]
    private void RemoveData()
    {
        if (SelectedData == null)
            return;
        int? lastIndex = SelectedDataIndex;
        var selectedData = SelectedData;
        // Issue Command
        CommandSequence command = new();
        command.Add(new CustomCommand(selectedData.Unregister, selectedData.Register)); // Unregister from the datamodel
        command.Add(new RemoveFromCollectionCommand<IDataViewModel>(Data, selectedData)); // Remove from the collection
        command.AddCleanup(selectedData.NotifyChange); // Notify any readonly selectors that might be listening
        _commandStack.IssueCommand(command);
        // Cleanup
        FixIndexAfterDelete(lastIndex); // Fix Index
    }
    #endregion

    #region Utilities
    /// <summary>
    /// Pick a reasonable valid selected item index
    /// when the number of items in the collection
    /// changes.
    /// </summary>
    /// <param name="lastIndex"></param>
    public void FixIndexAfterDelete(int? lastIndex)
    {
        try
        {
            _ = SearchFilteredData[(int)lastIndex - 1];
            SelectedDataIndex = lastIndex - 1;
            return;
        }
        catch
        {
            if (Data.Count > 0)
                SelectedDataIndex = 0;
        }
    }
    public void FixIndexAfterUndo(int? lastIndex)
    {
        try
        {
            _ = SearchFilteredData[(int)lastIndex];
            SelectedDataIndex = lastIndex;
            return;
        }
        catch
        {
            FixIndexAfterDelete(lastIndex); // This is confusing and it needs to be more clear what the thought process is here
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