using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Commands;
using MolluskEditor.Factories;
using MolluskEditor.Services;
using MolluskEngine.Extensions;

namespace MolluskEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private EditorViewModel _currentEditor;
    private EditorFactory _editorFactory;
    private WindowFactory _windowFactory;
    private SaveLoadService _saveLoadService;
    private CommandStack _commandStack;
    private List<ChildWindowViewModel> _childWindows = [];
    public IEnumerable<EditorName?> ChildWindows { get {
        return _childWindows.Select(n => n.EditorName);}}
    public IEnumerable<EditorName> VisibleEditorTabs { get{
        return EditorName.Values().Where(n => !ChildWindows.Contains(n)); }}
    
    public MainWindowViewModel(EditorFactory editorFactory, WindowFactory windowFactory, 
        SaveLoadService saveLoadService, CommandStack commandStack)
    {
        _editorFactory = editorFactory;
        _windowFactory = windowFactory;
        _saveLoadService = saveLoadService;
        _commandStack = commandStack;
        GoToUnits();
    }
    [RelayCommand]
    private void SaveProject() { _saveLoadService.Save(); }
    [RelayCommand]
    private void LoadProject() { _saveLoadService.Open(); }
    [RelayCommand]
    private void Undo() { _commandStack.Undo(); }
    [RelayCommand]
    private void Redo() { _commandStack.Redo(); }
    /// <summary>
    /// Pops out the current editor as a new window
    /// </summary>
    [RelayCommand]
    private void EjectEditor()
    {
        if (VisibleEditorTabs.Count() <= 1)
            return;
        var childWindow = _windowFactory.LaunchNewChildWindow(_currentEditor.EditorName);
        _childWindows.Add(childWindow);
        childWindow.ChildWindowClosed += ChildWindowClosed;
        GoToFirstTab();
        RefreshEditorTabs();
    }
    private void GoToFirstTab()
    {
        try { CurrentEditor.Dispose(); } catch {}
        CurrentEditor = _editorFactory.GetEditorViewModel(
            VisibleEditorTabs.ElementAt(0));
    }
    private void RefreshEditorTabs()
    {
        UnitsTabVisible = VisibleEditorTabs.Contains(EditorName.Units);
        TerrainTabVisible = VisibleEditorTabs.Contains(EditorName.Terrain);
        TilesetsTabVisible = VisibleEditorTabs.Contains(EditorName.Tilesets);
        MapsTabVisible = VisibleEditorTabs.Contains(EditorName.Maps);
    }
    private void ChildWindowClosed(object? sender, EventArgs args)
    {
        var senderChildWindow = (ChildWindowViewModel?)sender;
        if (senderChildWindow == null)
            return; // But this should never happen
        _childWindows.Remove(senderChildWindow);
        RefreshEditorTabs();
    }
    [RelayCommand]
    private void GoToUnits()
    {
        if (CurrentEditor?.EditorName == EditorName.Units) return;
        try { CurrentEditor.Dispose(); } catch {}
        CurrentEditor = _editorFactory.GetEditorViewModel(EditorName.Units);
    }
    [ObservableProperty]
    private bool _unitsTabVisible = true;
    [RelayCommand]
    private void GoToTerrain()
    {
        if (CurrentEditor?.EditorName == EditorName.Terrain) return;
        try { CurrentEditor.Dispose(); } catch {}
        CurrentEditor = _editorFactory.GetEditorViewModel(EditorName.Terrain);
    }
    [ObservableProperty]
    private bool _terrainTabVisible = true;
    [RelayCommand]
    private void GoToTilesets()
    {
        if (CurrentEditor?.EditorName == EditorName.Tilesets) return;
        try { CurrentEditor.Dispose(); } catch {}
        CurrentEditor = _editorFactory.GetEditorViewModel(EditorName.Tilesets);
    }
    [ObservableProperty]
    private bool _tilesetsTabVisible = true;
    [RelayCommand]
    private void GoToMaps()
    {
        if (CurrentEditor?.EditorName == EditorName.Maps) return;
        try { CurrentEditor.Dispose(); } catch {}
        CurrentEditor = _editorFactory.GetEditorViewModel(EditorName.Maps);
    }
    [ObservableProperty]
    private bool _mapsTabVisible = true;
}
