using System;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MolluskEditor.Wrappers;

/// <summary>
/// Short for "Observable Value". Wrapper class for primitives
/// (e.g. int) to implement INotifyPropertyChanged.
/// </summary>
public partial class ObsVal<T> : ObservableObject
{
    [ObservableProperty]
    private T _value;
    public ObsVal(T value)
    {
        Value = value;
    }
}
