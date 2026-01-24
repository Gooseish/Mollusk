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
    
    public MainWindowViewModel(EditorFactory editorFactory, WindowFactory windowFactory)
    {
        _editorFactory = editorFactory;
        _windowFactory = windowFactory;
        GoToUnits();
    }
    [RelayCommand]
    private void SaveProject()
    {
        SaveLoadService.Save();
    }
    [RelayCommand]
    private void LoadProject()
    {
        SaveLoadService.Open();
    }
    /// <summary>
    /// Pops out the current editor as a new window
    /// </summary>
    [RelayCommand]
    private void EjectEditor()
    {
        //CurrentEditor.Dispose();
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
