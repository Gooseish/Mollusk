using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using MolluskEditor.ViewModels;

namespace MolluskEditor.Views;

public partial class MapsEditorView : UserControl
{
    private bool _isDrawing;
    public MapsEditorView()
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
        _isDrawing = true;
        if (DataContext is MapsEditorViewModel mapsEditorView)
        {
            // Tile painter
            mapsEditorView.BeginPainting();
            mapsEditorView.PaintTilemap(e.GetPosition(DrawingCanvas));

            // Tile picker
            mapsEditorView.PickTile(e.GetPosition(TilePicker));
        }
    }
    private void OnRightMouseButtonPressed(PointerPressedEventArgs e)
    {
        // Tile picker
        if (DataContext is MapsEditorViewModel mapsEditorView)
            mapsEditorView.SampleTilemap(e.GetPosition(DrawingCanvas));
    }
    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_isDrawing) return;
        if (!e.GetCurrentPoint(sender as Control).Properties.IsLeftButtonPressed)
            return;
        // Tile painter
        if (DataContext is MapsEditorViewModel mapsEditorView)
            mapsEditorView.PaintTilemap(e.GetPosition(DrawingCanvas));
    }
    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _isDrawing = false;
        var properties = e.GetCurrentPoint(this).Properties;
        if (DataContext is MapsEditorViewModel mapsEditorViewModel)
            if (properties.IsLeftButtonPressed == false && e.InitialPressMouseButton == MouseButton.Left)
                mapsEditorViewModel.FinishPainting();
    }
}