using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MolluskEditor.ViewModels;

namespace MolluskEditor.Views;

public partial class EditorWindowView : Window
{
    public EditorWindowView()
    {
        InitializeComponent();
    }
    public void Subscribe()
    {
        if (DataContext != null)
            Closed += ((EditorWindowViewModel)DataContext).OnClose;
    }
}