using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MolluskEditor.ViewModels;

namespace MolluskEditor.Views;

public partial class ChildWindowView : Window
{
    public ChildWindowView()
    {
        InitializeComponent();
        if (DataContext != null)
            Closed += ((ChildWindowViewModel)DataContext).OnClose;
    }
}