using System;
using MolluskEditor.Commands;

namespace MolluskEditor.ViewModels;

public partial class UnitsEditorViewModel : EditorViewModel
{
    public string Test {get;set;} = "Units";
    public UnitsEditorViewModel(CommandStack commandStack)
        : base(commandStack)
    {
        EditorName = EditorName.Units;
    }

    public override void Dispose()
    {
        throw new NotImplementedException();
    }
}