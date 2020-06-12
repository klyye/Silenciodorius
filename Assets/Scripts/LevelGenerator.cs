using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;
using static GameException;
using static Utility;

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
    ///     The number of chunks generated that the player can walk in.
    /// </summary>
    private uint _pathableChunks;

    /// <summary>
    ///     A mapping of valid spawn positions to the directions to enter them from.
    /// </summary>
    private IDictionary<Vector2Int, Direction> _spawnPositions;

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
    public Vector2Int dimensions;

    /// <summary>
    ///     The name of the empty GameObject that acts as the parent to the level.
    /// </summary>
    public string holderName;

    /// <summary>
    ///     The number of iterations for the algorithm to take. The more iterations, the more rooms.
    /// </summary>
    public uint iterations;

    /// <summary>
    ///     The minimum number of pathable chunks to be generated.
    /// </summary>
    public uint minChunks;

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
    /// <returns>The empty Transform that holds all of the map objects.</returns>
    public Transform GenerateLevel()
    {
        ValidateVariables();
        Random.InitState(seed);
        _pathableChunks = 0;
        while (_pathableChunks < minChunks)
        {
            _levelLayout = new Chunk[dimensions.x, dimensions.y];
            LayoutBorderWall(RandomElement(possibleWalls));
            _pathableChunks = 0;

            var startPos = new Vector2Int(Random.Range(1, dimensions.x - 1),
                Random.Range(1, dimensions.y - 1));
            var startChunk = RandomElement(possibleStartRooms);
            _levelLayout[startPos.x, startPos.y] = startChunk;
            _pathableChunks++;
            _spawnPositions = new Dictionary<Vector2Int, Direction>();
            AddAdjacentPositions(startPos);
            for (var i = 0; i < iterations; i++)
                GenerateLayer(i % 2 == 0 ? (Chunk[]) possibleCorridors : possiblePathRooms);
            //Print2DArray(_levelLayout);

            var stairPos = RandomEntry(_spawnPositions).Key;
            _levelLayout[stairPos.x, stairPos.y] = RandomElement(possibleStairRooms);
            _pathableChunks++;
        }

        FillWithWalls();
        return InstantiateLevelLayout();
    }

    /// <summary>
    ///     Adds all of the openings of the chunk at startPos to _spawnPositions.
    /// </summary>
    /// <param name="startPos">The position of chunk whose adjacent positions to enqueue.</param>
    private void AddAdjacentPositions(Vector2Int startPos)
    {
        var startChunk = _levelLayout[startPos.x, startPos.y];
        foreach (var dir in ShuffleArray(startChunk.openings))
        {
            var checkPos = startPos + dir.ToVector2();
            if (WithinBounds(checkPos) && !_levelLayout[checkPos.x, checkPos.y] &&
                !_spawnPositions.ContainsKey(checkPos))
                _spawnPositions.Add(checkPos, dir.Opposite());
        }
    }

    /// <summary>
    ///     Adds a layer of chunks around the level using the positions and directions in
    ///     _spawnPositions. Updates _spawnPositions accordingly.
    /// </summary>
    /// <param name="possibleChunks">All possible chunks to spawn in this layer.</param>
    private void GenerateLayer(Chunk[] possibleChunks)
    {
        for (var i = 0; _spawnPositions.Count > 1 && i < chunksPerIteration; i++)
        {
            var nextEntry = RandomEntry(_spawnPositions);
            var nextPos = nextEntry.Key;
            var nextDir = nextEntry.Value;
            _spawnPositions.Remove(nextPos);
            var nextChunk = RandomElement(possibleChunks);
            foreach (var c in ShuffleArray(possibleChunks))
            {
                if (ExtendsPath(nextChunk, nextDir, nextPos)) break;
                nextChunk = c;
            }

            _levelLayout[nextPos.x, nextPos.y] = nextChunk;
            _pathableChunks++;
            AddAdjacentPositions(nextPos);
        }
    }

    /// <summary>
    ///     Checks if putting the chunk CHUNK at position POS results in CHUNK having an unblocked
    ///     opening.
    /// </summary>
    /// <param name="chunk">The type of chunk to place.</param>
    /// <param name="inDir">The direction of opening that CHUNK must have.</param>
    /// <param name="pos">The position on _levelLayout that we're placing CHUNK at.</param>
    /// <returns>Whether putting CHUNK at POS coming in from INDIR extends the path.</returns>
    private bool ExtendsPath(Chunk chunk, Direction inDir, Vector2Int pos)
    {
        var entrance = false;
        var openPath = false;
        foreach (var dir in chunk.openings)
        {
            if (dir == inDir) entrance = true;
            var checkPos = pos + dir.ToVector2();
            if (!_levelLayout[checkPos.x, checkPos.y]) openPath = true;
        }

        return entrance && openPath;
    }

    /// <summary>
    ///     Goes through every element in _levelLayout and instantiates it at the proper position!
    /// </summary>
    /// <returns>The empty transform that holds the instantiated map.</returns>
    private Transform InstantiateLevelLayout()
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

        return levelHolder;
    }

    /// <summary>
    ///     Updates _levelLayout to have a border of walls surrounding it.
    /// </summary>
    private void LayoutBorderWall(Wall wall)
    {
        for (var x = 0; x < dimensions.x; x++)
            _levelLayout[x, dimensions.y - 1] = _levelLayout[x, 0] = wall;
        for (var y = 0; y < dimensions.y; y++)
            _levelLayout[dimensions.x - 1, y] = _levelLayout[0, y] = wall;
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
    private bool WithinBounds(Vector2Int pos)
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
            throw Error("Can't have negative dimensions.");
    }
}