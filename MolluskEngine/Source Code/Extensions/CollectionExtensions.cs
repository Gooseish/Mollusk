using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace MolluskEngine.Extensions;

public static class CollectionExtensions
{ 
    public static T[,] To2DArray<T>(this IEnumerable<T> source, int rows, int columns)
    {
        if (rows * columns != source.Count())
            throw new ArgumentException("Source collection length does not match array size.");
        
        T[,] result = new T[rows, columns];
        int i = 0;
        for (int j = 0; j < rows; j++)
            for (int k = 0; k < columns; k++)
            {
                result[j, k] = source.ElementAt(i);
                i++;
            }
        return result;
    }
}
