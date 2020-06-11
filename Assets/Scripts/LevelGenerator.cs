﻿using System;
using System.Collections.Generic;
using Boo.Lang.Runtime;
using ExtensionMethods;
using UnityEngine;
using static Utility;
using Random = UnityEngine.Random;

/// <summary>
///     Generates a 2D array of Chunks that represents each level of the dungeon.
/// </summary>
public class LevelGenerator : MonoBehaviour
{
    /// <summary>
    ///     The layout of the level, where _levelLayout[x, y] represents the Chunk type at (x, y).
    /// </summary>
    private Chunk[,] _levelLayout;

    /// <summary>
    ///     A queue of valid positions to spawn chunks. Each tuple represents a position and the
    ///     direction to connect the tuple from.
    /// </summary>
    private Queue<Tuple<Vector2, Direction>> _spawnPositions;

    /// <summary>
    ///     The size to scale each chunk by.
    /// </summary>
    public float chunkSize;

    /// <summary>
    ///     The number of chunks added per iteration of the algorithm.
    /// </summary>
    public uint chunksPerIteration;

    /// <summary>
    ///     dimensions.x -> the maximum width of the map
    ///     dimensions.y -> the maximum height of the map
    /// </summary>
    public Vector2 dimensions;

    /// <summary>
    ///     The name of the empty GameObject that acts as the parent to the level.
    /// </summary>
    public string holderName;

    /// <summary>
    ///     The number of iterations for the algorithm to take. The more iterations, the more rooms.
    /// </summary>
    public uint iterations;

    /// <summary>
    ///     All of the possible corridor chunks that can be spawned.
    /// </summary>
    public Corridor[] possibleCorridors;

    /// <summary>
    ///     All of the possible room chunks that can be spawned, excluding start and stair rooms.
    /// </summary>
    public Room[] possiblePathRooms;

    /// <summary>
    ///     All of the possible stair rooms that can be generated.
    /// </summary>
    public Room[] possibleStairRooms;

    /// <summary>
    ///     All of the possible stair rooms that can be generated.
    /// </summary>
    public Room[] possibleStartRooms;

    /// <summary>
    ///     All of the possible wall chunks that can be spawned.
    /// </summary>
    public Wall[] possibleWalls;

    /// <summary>
    ///     The seed for the pseudorandom number generator that determines this level's randomness.
    /// </summary>
    public int seed;

    /// <summary>
    ///     Instantiates the dungeon level according to the public variables.
    /// </summary>
    public void GenerateLevel()
    {
        ValidateVariables();
        _levelLayout = new Chunk[(int) dimensions.x, (int) dimensions.y];
        Random.InitState(seed);
        LayoutBorderWall(RandomElement(possibleWalls));

        var startPos = new Vector2(Random.Range(1, (int) dimensions.x - 1),
            Random.Range(1, (int) dimensions.y - 1));
        var startChunk = RandomElement(possibleStartRooms);
        _levelLayout[(int) startPos.x, (int) startPos.y] = startChunk;
        _spawnPositions = new Queue<Tuple<Vector2, Direction>>();
        EnqueueAdjacentPositions(startPos);
        for (var i = 0; i < iterations; i++)
            GenerateLayer(i % 2 == 0 ? (Chunk[]) possibleCorridors : possiblePathRooms);

        var stairPos = _spawnPositions.Dequeue().Item1;
        _levelLayout[(int) stairPos.x, (int) stairPos.y] = RandomElement(possibleStairRooms);
        FillWithWalls();
        InstantiateLevelLayout();
    }

    /// <summary>
    ///     Adds all of the openings of the chunk at startPos to _spawnPositions.
    /// </summary>
    /// <param name="startPos">The position of chunk whose adjacent positions to enqueue.</param>
    private void EnqueueAdjacentPositions(Vector2 startPos)
    {
        var startChunk = _levelLayout[(int) startPos.x, (int) startPos.y];
        foreach (var dir in ShuffleArray(startChunk.openings))
        {
            var checkPos = startPos + dir.ToVector2();
            if (WithinBounds(checkPos) && !_levelLayout[(int) checkPos.x, (int) checkPos.y])
                _spawnPositions.Enqueue(new Tuple<Vector2, Direction>(checkPos, dir.Opposite()));
        }
    }

    /// <summary>
    ///     Adds a layer of chunks around the level using the positions and directions in
    ///     _spawnPositions. Updates _spawnPositions accordingly.
    /// </summary>
    /// <param name="possibleChunks">All possible chunks to spawn in this layer.</param>
    private void GenerateLayer(Chunk[] possibleChunks)
    {
        for (var i = 0; _spawnPositions.Count > 0 && i < chunksPerIteration; i++)
        {
            var nextPosDir = _spawnPositions.Dequeue();
            var nextPos = nextPosDir.Item1;
            var nextDir = nextPosDir.Item2;
            while (_levelLayout[(int) nextPos.x, (int) nextPos.y])
            {
                nextPosDir = _spawnPositions.Dequeue();
                nextPos = nextPosDir.Item1;
                nextDir = nextPosDir.Item2;
            }

            var nextChunk = RandomElement(possibleChunks);
            while (!Array.Exists(nextChunk.openings, d => d == nextDir))
                nextChunk = RandomElement(possibleChunks);
            _levelLayout[(int) nextPos.x, (int) nextPos.y] = nextChunk;
            EnqueueAdjacentPositions(nextPos);
        }
    }

    /// <summary>
    ///     Goes through every element in _levelLayout and instantiates it at the proper position!
    /// </summary>
    private void InstantiateLevelLayout()
    {
        var oldMap = transform.Find(holderName);
        if (oldMap) DestroyImmediate(oldMap.gameObject);
        var levelHolder = new GameObject(holderName).transform;
        levelHolder.parent = transform;
        for (var x = 0; x < dimensions.x; x++)
        for (var y = 0; y < dimensions.y; y++)
        {
            var spawnPos = Chunk.LENGTH * chunkSize * new Vector2(x, y);
            var chunkPrefab = _levelLayout[x, y];
            var spawnedChunk = Instantiate(chunkPrefab, spawnPos, Quaternion.identity);
            spawnedChunk.transform.parent = levelHolder;
            spawnedChunk.transform.localScale = chunkSize * Vector2.one;
        }
    }

    /// <summary>
    ///     Updates _levelLayout to have a border of walls surrounding it.
    /// </summary>
    private void LayoutBorderWall(Wall wall)
    {
        for (var x = 0; x < dimensions.x; x++)
            _levelLayout[x, (int) dimensions.y - 1] = _levelLayout[x, 0] = wall;
        for (var y = 0; y < dimensions.y; y++)
            _levelLayout[(int) dimensions.x - 1, y] = _levelLayout[0, y] = wall;
    }

    /// <summary>
    ///     Fills in the remaining empty chunks of _levelLayouts[x, y] with random walls.
    /// </summary>
    private void FillWithWalls()
    {
        for (var x = 0; x < dimensions.x; x++)
        for (var y = 0; y < dimensions.y; y++)
            if (!_levelLayout[x, y])
                _levelLayout[x, y] = RandomElement(possibleWalls);
    }


    /// <summary>
    ///     Checks if _levelLayout[pos.x, pos.y] gives array out of bounds exception.
    /// </summary>
    /// <param name="pos">The position to check.</param>
    /// <returns>True if no exception would be thrown, false otherwise.</returns>
    private bool WithinBounds(Vector2 pos)
    {
        return pos.x >= 0 && pos.x <= dimensions.x && pos.y >= 0 && pos.y <= dimensions.y;
    }


    /// <summary>
    ///     Throws exceptions of public variables are set to invalid values. This system of error
    ///     checking seems a bit bad, is there a way to restrict one variable to another in the
    ///     editor?
    /// </summary>
    private void ValidateVariables()
    {
        if (dimensions.x < 0 || dimensions.y < 0)
            throw new RuntimeException("Can't have negative dimensions.");

        dimensions.x = Mathf.Round(dimensions.x);
        dimensions.y = Mathf.Round(dimensions.y);
    }
}