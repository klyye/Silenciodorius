using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO FINISH THIS LATER
public class Level : MonoBehaviour
{
    private Chunk[,] _levelLayout;

    public Chunk[,] LevelLayout
    {
        set => _levelLayout = value;
    }

    /// <summary>
    ///     Instantiates chunks based on the data in _levelLayout.
    /// </summary>
    public void InstantiatePrefabs(float chunkSize)
    {
        for (var x = 0; x < _levelLayout.GetLength(0); x++)
        for (var y = 0; y < _levelLayout.GetLength(1); y++)
        {
            var spawnPos = Chunk.LENGTH * chunkSize * new Vector2(x, y);
            var chunkPrefab = _levelLayout[x, y];
            var spawnedChunk = Instantiate(chunkPrefab, spawnPos, Quaternion.identity);
            spawnedChunk.transform.localScale = chunkSize * Vector2.one;
        }
    }
}
