using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
}
