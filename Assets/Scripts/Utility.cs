using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
///     A class of generally useful functions that don't belong in any particular class.
/// </summary>
public static class Utility
{
    /// <summary>
    ///     Uses UnityEngine.Random to get a random element in range [MIN, MAX) in ARR. You must set
    ///     the seed beforehand if you want consistent randomness.
    ///     The float version of Random.Range has the maximum INCLUSIVE, but the int overloaded
    ///     version of that same function has the maximum EXCLUSIVE. That's not confusing at all!
    /// </summary>
    /// <param name="min">The minimum index in the range. (inclusive)</param>
    /// <param name="max">The maximum index in the range. (exclusive)</param>
    /// <param name="arr">The array to select a random value from.</param>
    /// <typeparam name="T">The type of the array.</typeparam>
    /// <returns>A random value with index in [MIN, MAX] of array ARR.</returns>
    public static T RandomElement<T>(int min, int max, T[] arr)
    {
        return arr[Random.Range(min, max)];
    }

    /// <summary>
    ///     Uses UnityEngine.Random to get a random element from ARR. You must set the seed
    ///     beforehand if you want consistent randomness.
    /// </summary>
    /// <param name="arr">The array to get a random element from.</param>
    /// <typeparam name="T">The type of the array.</typeparam>
    /// <returns>A random element from the array.</returns>
    public static T RandomElement<T>(T[] arr)
    {
        return RandomElement(0, arr.Length, arr);
    }

    /// <summary>
    ///     Uses UnityEngine.Random to get a random element from ARR that satisfies PRED. You must
    ///     set the seed beforehand if you want consistent randomness.
    /// </summary>
    /// <param name="arr">The array to select an element from.</param>
    /// <param name="pred">The condition that the random element must satisfy.</param>
    /// <typeparam name="T">The type of the array.</typeparam>
    /// <returns>
    ///     A random element x from ARR such that pred(x) is true, if one exists.
    ///     default(T) otherwise.
    /// </returns>
    public static T RandomElement<T>(T[] arr, Predicate<T> pred)
    {
        var filtered = Array.FindAll(arr, pred);
        return filtered.Length > 0 ? RandomElement(filtered) : default;
    }

    /// <summary>
    ///     Swaps the elements of ARR at indices I and J.
    /// </summary>
    /// <param name="arr">The array whose elements should be swapped.</param>
    /// <param name="i">The first index to swap.</param>
    /// <param name="j">The second index to swap.</param>
    /// <typeparam name="T">The type of the array.</typeparam>
    public static void SwapElements<T>(T[] arr, int i, int j)
    {
        var temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    /// <summary>
    ///     What do you think this does??????
    /// </summary>
    /// <param name="arr">The array to shuffle.</param>
    /// <typeparam name="T">The type of the array.</typeparam>
    /// <returns>A shuffled version of ARR. ARR remains unchanged.</returns>
    public static T[] ShuffleArray<T>(T[] arr)
    {
        var output = new T[arr.Length];
        Array.Copy(arr, output, arr.Length);
        for (var i = 0; i < arr.Length - 1; i++)
            SwapElements(output, i, Random.Range(i, arr.Length));

        return output;
    }

    /// <summary>
    ///     Prints the contents of a 2d array.
    /// </summary>
    /// <param name="arr">The 2d array to print.</param>
    /// <typeparam name="T">The type of the array.</typeparam>
    public static void Print2DArray<T>(T[,] arr)
    {
        var builder = new StringBuilder();
        for (var x = 0; x < arr.GetLength(0); x++)
        {
            builder.Append("[");
            for (var y = 0; y < arr.GetLength(1); y++)
            {
                builder.Append(arr[x, y]);
                builder.Append(",\t");
            }

            builder.Append("],\n");
        }

        MonoBehaviour.print(builder);
    }

    /// <summary>
    ///     Uses UnityEngine.Random to get a random element from a Dictionary.
    /// </summary>
    /// <param name="dict">The dictionary to get a random element from.</param>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TVal">The type of the values in the dictionary.</typeparam>
    /// <returns>A random KeyValuePair from the dictionary.</returns>
    public static KeyValuePair<TKey, TVal> RandomEntry<TKey, TVal>(IDictionary<TKey, TVal> dict)
    {
        var entryNum = Random.Range(1, dict.Count);
        var iter = dict.GetEnumerator();
        for (var i = 0; i < entryNum; i++) iter.MoveNext();

        return iter.Current;
    }
}