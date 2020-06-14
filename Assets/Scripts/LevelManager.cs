using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
///     Advances and instantiates the current level.
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
    ///     The name of the gameObject that parents all of the instantiated prefabs.
    /// </summary>
    private const string HolderName = "Level Holder";

    /// <summary>
    ///     The currently loaded level.
    /// </summary>
    private Level _currLevel;

    /// <summary>
    ///     How many floors we have advanced past.
    /// </summary>
    private int _depth;

    /// <summary>
    ///     Generates the level!
    /// </summary>
    private LevelGenerator _levelGen;

    /// <summary>
    ///     The object representing the player of the game.
    /// </summary>
    private Player _player;

    /// <summary>
    ///     The size to scale each chunk by.
    /// </summary>
    public float chunkSize;

    private void Start()
    {
        _depth = 0;
        _player = FindObjectOfType<Player>();
        _player.OnStairReached += AdvanceLevel;
        _levelGen = GetComponent<LevelGenerator>();
        _levelGen.seed = Random.Range(int.MinValue, int.MaxValue); //used random to set random lmao
        AdvanceLevel();
    }

    /// <summary>
    ///     Adjusts the parameters of _levelGen in order to get more difficult with depth.
    /// </summary>
    private void GenerateCurrentLevel()
    {
        _levelGen.dimensions = new Vector2Int(_depth / 5 + 5, _depth / 5 + 5);
        _levelGen.iterations = _depth;
        _levelGen.chunksPerIteration = 2;
        _levelGen.minChunks = 7;
        _currLevel = _levelGen.GenerateLevel(); // TODO replace hardcoded values
        print("Depth:" + _depth);
    }

    /// <summary>
    ///     Increases the depth and generates an appropriate level for that depth.
    /// </summary>
    private void AdvanceLevel()
    {
        _depth++;
        GenerateCurrentLevel();
        InstantiateCurrentLevel();
        var spawnCoords = _currLevel.spawnPoint;
        _player.transform.position = chunkSize * Chunk.LENGTH *
                                     new Vector2(spawnCoords.x, spawnCoords.y);
    }

    private void OnDestroy()
    {
        _player.OnStairReached -= AdvanceLevel;
    }


    /// <summary>
    ///     Instantiates chunks based on the data in _currLevel.
    /// </summary>
    public void InstantiateCurrentLevel()
    {
        var oldMap = transform.Find(HolderName);
        if (oldMap) Destroy(oldMap.gameObject);
        var mapHolder = new GameObject(HolderName).transform;
        mapHolder.parent = transform;

        for (var x = 0; x < _currLevel.levelLayout.GetLength(0); x++)
        for (var y = 0; y < _currLevel.levelLayout.GetLength(1); y++)
        {
            var spawnPos = Chunk.LENGTH * chunkSize * new Vector2(x, y);
            var chunkPrefab = _currLevel.levelLayout[x, y];
            var spawnedChunk = Instantiate(chunkPrefab, spawnPos, Quaternion.identity).transform;
            spawnedChunk.localScale = chunkSize * Vector2.one;
            spawnedChunk.parent = mapHolder;
        }
    }
}