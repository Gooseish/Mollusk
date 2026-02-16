using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using MolluskEditor.ViewModels;
using MolluskEditor.Views;
using System;
using MolluskEditor.Factories;
using Microsoft.Extensions.DependencyInjection;
using MolluskEditor.Models;
using MolluskEditor.Services;
using MolluskEngine.GameBoard;
using MolluskEditor.Commands;

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
            collection.AddSingleton<CommandStack>();
            collection.AddSingleton<MainWindowViewModel>();
            collection.AddTransient<TerrainEditorViewModel>();
            collection.AddTransient<TilesetEditorViewModel>();
            collection.AddTransient<UnitsEditorViewModel>();
            collection.AddTransient<MapsEditorViewModel>();
            collection.AddTransient<ChildWindowView>();
            collection.AddTransient<ChildWindowViewModel>();

            // Services
            collection.AddSingleton<SaveLoadService>();

            // Data Models
            collection.AddSingleton<DataModel<Terrain>>();
            collection.AddSingleton<DataModel<Tileset>>();
            collection.AddSingleton<DataModel<GameMap>>();

            // Editor Factory
            collection.AddSingleton<Func<EditorName, EditorViewModel>>(x => name => name switch
            {
                EditorName.Terrain => x.GetRequiredService<TerrainEditorViewModel>(),
                EditorName.Units => x.GetRequiredService<UnitsEditorViewModel>(),
                EditorName.Tilesets  => x.GetRequiredService<TilesetEditorViewModel>(),
                EditorName.Maps => x.GetRequiredService<MapsEditorViewModel>(),
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
                resultView.Subscribe();
                return resultView;
            });
            collection.AddSingleton<WindowFactory>();

            var services = collection.BuildServiceProvider();

            DataViewModelDependencyInjection(services);

            desktop.MainWindow = new MainWindow
            {
                DataContext = services.GetRequiredService<MainWindowViewModel>()
            };
        }
        base.OnFrameworkInitializationCompleted();
    }
    private void DataViewModelDependencyInjection(ServiceProvider services)
    {
        // Tell the data view models about the data model singletons and the command stack
        // This kind of sucks that I have to do it manually...
        TerrainDataViewModel.InjectDependency(
                services.GetRequiredService<DataModel<Terrain>>(),
                services.GetRequiredService<CommandStack>());
        TilesetDataViewModel.InjectDependency(
            services.GetRequiredService<DataModel<Tileset>>(),
            services.GetRequiredService<DataModel<Terrain>>(),
            services.GetRequiredService<CommandStack>());
        MapDataViewModel.InjectDependency(
            services.GetRequiredService<DataModel<GameMap>>(),
            services.GetRequiredService<CommandStack>());
        TerrainTileViewModel.InjectDependency(
            services.GetRequiredService<DataModel<Terrain>>()
        );
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