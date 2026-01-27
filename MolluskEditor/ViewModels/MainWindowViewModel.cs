using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MolluskEditor.Factories;
using MolluskEditor.Services;

namespace MolluskEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private EditorViewModel _currentEditor;
    private EditorFactory _editorFactory;
    private WindowFactory _windowFactory;
    private SaveLoadService _saveLoadService;
    
    public MainWindowViewModel(EditorFactory editorFactory, WindowFactory windowFactory, SaveLoadService saveLoadService)
    {
        _editorFactory = editorFactory;
        _windowFactory = windowFactory;
        _saveLoadService = saveLoadService;
        GoToUnits();
    }
    [RelayCommand]
    private void SaveProject()
    {
        _saveLoadService.Save();
    }
    [RelayCommand]
    private void LoadProject()
    {
        _saveLoadService.Open();
    }
    /// <summary>
    /// Pops out the current editor as a new window
    /// </summary>
    [RelayCommand]
    private void EjectEditor()
    {
        //CurrentEditor.Dispose(); // Add this line when ejecting an editor removes it from the main window
        _windowFactory.LaunchNewChildWindow(_currentEditor.EditorName);
    }
    [RelayCommand]
    private void GoToUnits()
    {
        try {CurrentEditor.Dispose();}
        catch {}
        CurrentEditor = _editorFactory.GetEditorViewModel(Data.EditorName.Units);
    }
    [RelayCommand]
    private void GoToTerrain()
    {
        try {CurrentEditor.Dispose();}
        catch {}
        CurrentEditor = _editorFactory.GetEditorViewModel(Data.EditorName.Terrain);
    }
}
