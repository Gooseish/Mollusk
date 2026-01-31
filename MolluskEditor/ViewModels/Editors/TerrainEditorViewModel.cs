using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Commands;
using MolluskEditor.Models;
using MolluskEditor.Services;
using MolluskEngine.Extensions;
using MolluskEngine.GameBoard;

namespace MolluskEditor.ViewModels;

public partial class TerrainEditorViewModel : EditorViewModel
{
    private CommandStack _commandStack;
    DataModel<Terrain> _dataModel;
    [ObservableProperty]
    private DataSelectorViewModel _data;
    [ObservableProperty]
    private TerrainDataViewModel? _selectedTerrain;
    public TerrainEditorViewModel(CommandStack commandStack, DataModel<Terrain> dataModel)
    {
        _commandStack = commandStack;
        _dataModel = dataModel;
        EditorName = MolluskEditor.Data.EditorName.Terrain;
        Data = new(typeof(TerrainDataViewModel), TerrainDataViewModel.ReadExisting);
        Subscribe();
    }
    
    #region Event Handling
    private void OnSelectionChanged(object? sender, EventArgs args)
    {
        SelectedTerrain = (TerrainDataViewModel?)Data.SelectedData;
    }
    private void OnProjectLoaded(object? sender, EventArgs args)
    {
        Data.Initialize(); // Perhaps abstract this as well
    }
    private void OnUndoOrRedo(object? sender, EventArgs args)
    {
        int? selectedIndex = SelectedTerrain == null ? null : int.Parse(SelectedTerrain.Id);
        Data.Initialize();
        Data.FixIndexAfterUndo(selectedIndex);
    }
    private void Subscribe()
    {
        SaveLoadService.ProjectLoaded += OnProjectLoaded;
        Data.IndexChanged += OnSelectionChanged;
        _dataModel.IdsChanged += Data.SortDataEvent;
        _commandStack.OnUndo += OnUndoOrRedo;
        _commandStack.OnRedo += OnUndoOrRedo;
    }
    private void Unsubscribe()
    {
        SaveLoadService.ProjectLoaded -= OnProjectLoaded;
        Data.IndexChanged -= OnSelectionChanged; // This should be taken care of by the garbage collector, so maybe is unnecessary
        _dataModel.IdsChanged -= Data.SortDataEvent;
        _commandStack.OnUndo -= OnUndoOrRedo;
        _commandStack.OnRedo -= OnUndoOrRedo;
    }
    public override void Dispose()
    {
        Unsubscribe();
    }
    #endregion
    
    // Todo: These should be static and readonly. Not sure how to do the binding
    [ObservableProperty]
    private int _weatherCount = WeatherType.Count();
    [ObservableProperty]
    private int _movementCount = MovementType.Count();
    [ObservableProperty]
    private ObservableCollection<string> _weatherNames = WeatherType.Names().ToObservableCollection();
    [ObservableProperty]
    private ObservableCollection<string> _movementNames = MovementType.Names().ToObservableCollection();
}