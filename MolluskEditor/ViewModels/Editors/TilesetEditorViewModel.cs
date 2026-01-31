using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Commands;
using MolluskEditor.Models;
using MolluskEngine.GameBoard;
using MolluskEditor.Data;
using MolluskEditor.Services;

namespace MolluskEditor.ViewModels;

public partial class TilesetEditorViewModel : EditorViewModel
{
    private DataModel<Tileset> _dataModel;
    [ObservableProperty]
    private DataSelectorViewModel _data;
    [ObservableProperty]
    private TilesetDataViewModel? _selectedTileset;
    public TilesetEditorViewModel(CommandStack commandStack, DataModel<Tileset> dataModel)
        : base(commandStack)
    {
        _dataModel = dataModel;
        EditorName = EditorName.Tilesets;
        Data = new(typeof(TilesetDataViewModel), TilesetDataViewModel.ReadExisting);
        Subscribe();
    }

    #region Event Handling
    private void OnSelectionChanged(object? sender, EventArgs args)
    {
        SelectedTileset = (TilesetDataViewModel?)Data.SelectedData;
    }
    private void OnProjectLoaded(object? sender, EventArgs args)
    {
        Data.Initialize(); // Perhaps abstract this as well
    }
    private void OnUndoOrRedo(object? sender, EventArgs args)
    {
        int? selectedIndex = SelectedTileset == null ? null : int.Parse(SelectedTileset.Id);
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
        //Data.IndexChanged -= OnSelectionChanged; // This should be taken care of by the garbage collector, so maybe is unnecessary
        _dataModel.IdsChanged -= Data.SortDataEvent;
        _commandStack.OnUndo -= OnUndoOrRedo;
        _commandStack.OnRedo -= OnUndoOrRedo;
    }
    public override void Dispose()
    {
        Unsubscribe();
    }
    #endregion
}
