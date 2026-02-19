using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using MolluskEditor.ViewModels;

namespace MolluskEditor.Views;

public partial class TilesetEditorView : UserControl
{
    private bool _isDrawing;
    public TilesetEditorView()
    {
        InitializeComponent();
        PointerPressed += OnPointerPressed;
        PointerMoved += OnPointerMoved;
        PointerReleased += OnPointerReleased;
        
    }
    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(sender as Control).Properties.IsLeftButtonPressed)
            OnLeftMouseButtonPressed(e);
        if (e.GetCurrentPoint(sender as Control).Properties.IsRightButtonPressed)
            OnRightMouseButtonPressed(e);
    }
    private void OnLeftMouseButtonPressed(PointerPressedEventArgs e)
    {
        // Terrain painter
        _isDrawing = true;
        if (DataContext is TilesetEditorViewModel tilesetEditorViewModel)
        {
            tilesetEditorViewModel.BeginPainting();
            tilesetEditorViewModel.PaintTilemap(e.GetPosition(DrawingCanvas));
        }
    }
    private void OnRightMouseButtonPressed(PointerPressedEventArgs e)
    {
        // Terrain picker
        if (DataContext is TilesetEditorViewModel tilesetEditorViewModel)
            tilesetEditorViewModel.SampleTilemap(e.GetPosition(DrawingCanvas));
    }
    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_isDrawing) return;
        if (!e.GetCurrentPoint(sender as Control).Properties.IsLeftButtonPressed)
            return;
        // Terrain painter
        if (DataContext is TilesetEditorViewModel tilesetEditorViewModel)
            tilesetEditorViewModel.PaintTilemap(e.GetPosition(DrawingCanvas));
    }
    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _isDrawing = false;
        var properties = e.GetCurrentPoint(this).Properties;
        if (DataContext is TilesetEditorViewModel tilesetEditorViewModel)
            if (properties.IsLeftButtonPressed == false && e.InitialPressMouseButton == MouseButton.Left)
                tilesetEditorViewModel.FinishPainting();
    }
}