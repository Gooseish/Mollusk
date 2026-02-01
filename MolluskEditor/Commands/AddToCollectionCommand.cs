using System;
using System.Collections.Generic;

namespace MolluskEditor.Commands;

public class AddToCollectionCommand<T> : Command
{
    private ICollection<T> _collection;
    private T _element;
    public AddToCollectionCommand(ICollection<T> collection, T element)
    {
        _collection = collection;
        _element = element;
    }
    public override void Do()
    {
        _collection.Add(_element);
    }
    public override void Undo()
    {
        _collection.Remove(_element);
    }
}
public class RemoveFromCollectionCommand<T> : Command
{
    private ICollection<T> _collection;
    private T _element;
    public RemoveFromCollectionCommand(ICollection<T> collection, T element)
    {
        _collection = collection;
        _element = element;
    }
    public override void Do()
    {
        _collection.Remove(_element);
    }
    public override void Undo()
    {
        _collection.Add(_element);
    }
}
