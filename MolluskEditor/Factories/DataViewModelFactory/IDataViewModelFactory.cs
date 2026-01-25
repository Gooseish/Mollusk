using System;
using System.Collections.ObjectModel;
using MolluskEditor.Data;

namespace MolluskEditor.Factories;

public interface IDataViewModelFactory
{
    public IDataViewModel New();
    public ObservableCollection<IDataViewModel> ReadExisting();
}
