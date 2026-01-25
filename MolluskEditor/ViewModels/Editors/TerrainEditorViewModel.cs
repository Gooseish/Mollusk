using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MolluskEditor.Services;

namespace MolluskEditor.ViewModels;

public partial class TerrainEditorViewModel : EditorViewModel
{
    [ObservableProperty]
    private DataSelectorSidebarViewModel _data;
    public TerrainEditorViewModel()
    {
        EditorName = MolluskEditor.Data.EditorName.Terrain;
        Data = new DataSelectorSidebarViewModel();
        Data.Initialize();
        Subscribe();
    }
    
    #region Event Handling
    private void OnProjectLoaded(object? sender, EventArgs args)
    {
        Data.Initialize(); // Perhaps abstract this as well
    }
    private void Subscribe()
    {
        SaveLoadService.ProjectLoaded += OnProjectLoaded;
    }
    private void Unsubscribe()
    {
        SaveLoadService.ProjectLoaded -= OnProjectLoaded;
    }
    public override void Dispose()
    {
        Unsubscribe();
    }
    #endregion
}