using UnityEngine;

/// <summary>
///     A class of generally useful functions that don't belong in any particular class.
/// </summary>
public static class Utility
{
    /// <summary>
    ///     Uses UnityEngine.Random to get a random element in range [MIN, MAX) in ARR. You must set
    ///     the seed beforehand if you want consistent randomness.
    ///
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
}