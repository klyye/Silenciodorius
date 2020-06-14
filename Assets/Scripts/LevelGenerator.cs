using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;
using static Utility;

/// <summary>
///     Generates a 2D array of Chunks that represents each level of the dungeon.
/// </summary>
public class LevelGenerator : MonoBehaviour
{
    /// <summary>
    ///     The number of chunks added per iteration of the algorithm.
    /// </summary>
    private int _chunksPerIteration;

    /// <summary>
    ///     dimensions.x -> the maximum width of the map
    ///     dimensions.y -> the maximum height of the map
    /// </summary>
    private Vector2Int _dimensions;

    /// <summary>
    ///     The number of iterations for the algorithm to take. The more iterations, the more rooms.
    /// </summary>
    private int _iterations;

    /// <summary>
    ///     The layout of the level, where _levelLayout[x, y] represents the Chunk type at (x, y).
    /// </summary>
    private Chunk[,] _levelLayout;

    /// <summary>
    ///     Regenerate the level until there's more pathable chunks than this.
    /// </summary>
    private int _minChunks;

    /// <summary>
    ///     The number of chunks generated that the player can walk in.
    /// </summary>
    private uint _pathableChunks;

    /// <summary>
    ///     The seed for the pseudorandom number generator that determines this level's randomness.
    /// </summary>
    private int _seed;

    /// <summary>
    ///     A mapping of valid spawn positions to the directions to enter them from.
    /// </summary>
    private IDictionary<Vector2Int, Direction> _spawnPositions;

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

    public Vector2Int dimensions
    {
        get => _dimensions;
        set
        {
            _dimensions.x = Mathf.Max(5, value.x);
            _dimensions.y = Mathf.Max(5, value.y);
        }
    }

    public int chunksPerIteration
    {
        get => _chunksPerIteration;
        set => _chunksPerIteration = Mathf.Max(1, value);
    }

    public int iterations
    {
        get => _iterations;
        set => _iterations = Mathf.Max(1, value);
    }

    public int minChunks
    {
        get => _minChunks;
        set => _minChunks = Mathf.Min(value, iterations * chunksPerIteration);
    }

    public int seed
    {
        get => _seed;
        set
        {
            _seed = value;
            Random.InitState(seed);
        }
    }

    /// <summary>
    ///     Instantiates the dungeon level according to the public variables.
    /// </summary>
    /// <returns>The empty GameObject that holds all of the map objects.</returns>
    public Level GenerateLevel()
    {
        _pathableChunks = 0;
        var startPos = Vector2Int.zero;
        while (_pathableChunks < minChunks)
        {
            _levelLayout = new Chunk[dimensions.x, dimensions.y];
            LayoutBorderWall(RandomElement(possibleWalls));
            _pathableChunks = 0;

            startPos = new Vector2Int(Random.Range(2, dimensions.x - 2),
                Random.Range(2, dimensions.y - 2));
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
        var output = new Level(_levelLayout, startPos);
        return output;
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
            var nextChunk = RandomElement(possibleChunks, c => ExtendsPath(c, nextDir, nextPos));
            if (nextChunk == null) //use ??= once C# 8.0 is supported
                nextChunk = RandomElement(possibleChunks, c => c.HasOpening(nextDir));
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
}