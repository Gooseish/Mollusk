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
    
    private DataModel<Terrain> _dataModel;
    [ObservableProperty]
    private DataSelectorViewModel _data;
    [ObservableProperty]
    private TerrainDataViewModel? _selectedTerrain;
    public TerrainEditorViewModel(CommandStack commandStack, DataModel<Terrain> dataModel)
        : base(commandStack)
    {
        _dataModel = dataModel;
        EditorName = EditorName.Terrain;
        Data = new(typeof(TerrainDataViewModel), TerrainDataViewModel.ReadExisting,
            commandStack);
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
        //int? selectedIndex = Data.SelectedDataIndex;
        //Data.Initialize();
        //Data.FixIndexAfterUndo(selectedIndex);
        // Need to update every field of the terrain if it's at zero
    }
    private void Subscribe()
    {
        SaveLoadService.ProjectLoaded += OnProjectLoaded;
        _dataModel.IdsChanged += Data.SortDataEvent;
        _commandStack.OnUndo += OnUndoOrRedo;
        _commandStack.OnRedo += OnUndoOrRedo;
        // Garbage Collected
        Data.IndexChanged += OnSelectionChanged;
    }
    private void Unsubscribe()
    {
        SaveLoadService.ProjectLoaded -= OnProjectLoaded;
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