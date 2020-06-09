using System;
using UnityEngine;

/// <summary>
///     Represents a LENGTH x LENGTH section of lines. This is the basic unit used in MapGenerator.cs.
/// </summary>
public abstract class Chunk : MonoBehaviour
{

    /// <summary>
    ///     The length in tiles of the side of every chunk.
    /// </summary>
    public const int LENGTH = 10;
}