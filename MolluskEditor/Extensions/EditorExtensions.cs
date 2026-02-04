using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MolluskEditor.ViewModels;
using MolluskEditor.Wrappers;

namespace MolluskEditor.Extensions;

public static class EditorExtensions
{
    public static List<int>? ToIntList(
            this ObservableCollection<ObsVal<string>> source)
    {
        List<int> result = [];
        var unwrappedSource = source.Select(n => n.Value);
        foreach (string str in unwrappedSource)
        {
            if (!int.TryParse(str, out int intValue))
                return null;
            result.Add(intValue);
        }
        return result;
    }
    public static List<int>? ToIntList(this ObservableCollection<TerrainTileViewModel> source)
    {
        List<int> result = [];
        foreach (TerrainTileViewModel element in source)
        {
            if (element.Id == null) return null;
            result.Add((int)element.Id);
        }
        return result;
    }
    public static ObservableCollection<TerrainTileViewModel> ToTerrainTileViewModel(
        this int[] source)
    {
        ObservableCollection<TerrainTileViewModel> result = [];
        List<int> sourceList = [.. source];
        for(int n = 0; n < sourceList.Count; n++)
            result.Add(new TerrainTileViewModel(sourceList[n], n));
        return result;
    }
    public static ObservableCollection<ObsVal<string>> ToWrappedStringCollection(
        this int[,] source)
    {
        ObservableCollection<ObsVal<string>> result = [];
        List<int> sourceList = [.. source];
        foreach(int i in sourceList)
            result.Add(new ObsVal<string>(i.ToString()));
        return result;
    }
    public static ObservableCollection<ObsVal<string>> ToWrappedStringCollection(
        this int[] source)
    {
        ObservableCollection<ObsVal<string>> result = [];
        List<int> sourceList = [.. source];
        foreach(int i in sourceList)
            result.Add(new ObsVal<string>(i.ToString()));
        return result;
    }
    

    public static Bitmap BitmapFromColor(int width, int height, Color color)
    {
        var pixelFormat = PixelFormat.Bgra8888;
        var bytesPerPixel = 4; // Bgra8888 uses 4 bytes
        var stride = width * bytesPerPixel;
        var buffer = new byte[height * stride];
        for (int i = 0; i < buffer.Length; i += bytesPerPixel)
        {
            buffer[i    ] = color.B;
            buffer[i + 1] = color.G;
            buffer[i + 2] = color.R;
            buffer[i + 3] = color.A;
        }

        var bitmap = new WriteableBitmap(
            new Avalonia.PixelSize(width, height),
            new Avalonia.Vector(96, 96), // Standard DPI?
            pixelFormat,
            AlphaFormat.Premul);
        
        using (var frameBuffer = bitmap.Lock()) 
            { Marshal.Copy(buffer, 0, frameBuffer.Address, buffer.Length); } 

        return bitmap;
    }
}
