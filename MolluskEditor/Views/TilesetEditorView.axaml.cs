using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using MolluskEditor.ViewModels;

namespace MolluskEditor.Views;

public partial class TilesetEditorView : UserControl
{
    private Point _startPoint;
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
        _isDrawing = true;
        _startPoint = e.GetPosition(DrawingCanvas);
        if (DataContext is TilesetEditorViewModel tilesetEditorViewModel)
            tilesetEditorViewModel.PaintTilemap(e.GetPosition(DrawingCanvas));
    }
    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_isDrawing){ return; }
        if (DataContext is TilesetEditorViewModel tilesetEditorViewModel)
            tilesetEditorViewModel.PaintTilemap(e.GetPosition(DrawingCanvas));
    }
    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _isDrawing = false;
        if (DataContext is TilesetEditorViewModel tilesetEditorViewModel)
            tilesetEditorViewModel.FinishPainting();
    }
}