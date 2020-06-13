using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Represents the layout of one level.
///     TODO: do I need this class?
/// </summary>
public class Level
{
    public Chunk[,] levelLayout { get; }

    public Level(Chunk[,] layout) => levelLayout = layout;

    public Vector2 SpawnPoint()
    {
        return Vector2.zero;     // TODO 
    }
}
