using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls.Notifications;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Commands;
using MolluskEditor.Data;
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
        if (VisibleEditorTabs.Count() == 0)
            return;
        _windowFactory.LaunchNewChildWindow(_currentEditor.EditorName);
        GoToFirstTab();
    }
    private void GoToFirstTab()
    {
        try { CurrentEditor.Dispose(); } catch {}
        CurrentEditor = _editorFactory.GetEditorViewModel(
            VisibleEditorTabs.ElementAt(0));
    }
    [RelayCommand]
    private void GoToUnits()
    {
        try { CurrentEditor.Dispose(); } catch {}
        CurrentEditor = _editorFactory.GetEditorViewModel(EditorName.Units);
    }
    public bool UnitsTabVisible{ get { return VisibleEditorTabs.Contains(EditorName.Units); }}
    [RelayCommand]
    private void GoToTerrain()
    {
        try { CurrentEditor.Dispose(); } catch {}
        CurrentEditor = _editorFactory.GetEditorViewModel(EditorName.Terrain);
    }
    public bool TerrainTabVisible{ get { return VisibleEditorTabs.Contains(EditorName.Terrain); }}
}
