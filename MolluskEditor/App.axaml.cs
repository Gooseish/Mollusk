using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using MolluskEditor.ViewModels;
using MolluskEditor.Views;
using System;
using MolluskEditor.Data;
using MolluskEditor.Factories;
using Microsoft.Extensions.DependencyInjection;


namespace MolluskEditor;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            
            var collection = new ServiceCollection();
            collection.AddSingleton<MainWindowViewModel>();
            collection.AddTransient<TerrainEditorViewModel>();
            collection.AddTransient<UnitsEditorViewModel>();
            collection.AddTransient<ChildWindowView>();
            collection.AddTransient<ChildWindowViewModel>();

            // Editor Factory
            collection.AddSingleton<Func<EditorName, EditorViewModel>>(x => name => name switch
            {
                EditorName.Terrain => x.GetRequiredService<TerrainEditorViewModel>(),
                EditorName.Units => x.GetRequiredService<UnitsEditorViewModel>(),
                _ => throw new InvalidOperationException("Editor type not recognized by editor factory.")
            });
            collection.AddSingleton<EditorFactory>();

            // Window Factory
            collection.AddSingleton<Func<EditorName, ChildWindowView>>(x => name =>
            {
                ChildWindowView resultView = x.GetRequiredService<ChildWindowView>();
                ChildWindowViewModel resultViewModel = x.GetRequiredService<ChildWindowViewModel>();
                resultViewModel.GoToEditor(name); // Is there a way to handle this in the constructor?
                resultView.DataContext = resultViewModel;
                return resultView;
            });
            collection.AddSingleton<WindowFactory>();

            var services = collection.BuildServiceProvider();

            desktop.MainWindow = new MainWindow
            {
                DataContext = services.GetRequiredService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}