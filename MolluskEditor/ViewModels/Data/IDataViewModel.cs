using System;

namespace MolluskEditor.Data;

public interface IDataViewModel
{
    public void Dispose();
    public int Id {get;set;}
}
