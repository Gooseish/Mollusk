using System;

namespace MolluskEditor.ViewModels;

public partial class UnitsEditorViewModel : EditorViewModel
{
    public string Test {get;set;} = "Units";
    public UnitsEditorViewModel()
    {
        EditorName = Data.EditorName.Units;
    }
}