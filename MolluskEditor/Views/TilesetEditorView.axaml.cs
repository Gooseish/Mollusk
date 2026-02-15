using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

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
        // Do stuff
    }
    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (_isDrawing)
        {
            // Do stuff
        }
    }
    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _isDrawing = false;
        // Do stuff
    }
}