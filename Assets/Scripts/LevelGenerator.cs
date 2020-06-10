using Boo.Lang.Runtime;
using UnityEngine;
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
    ///     The size to scale each chunk by.
    /// </summary>
    public float chunkSize;

    /// <summary>
    ///     The name of the empty GameObject that acts as the parent to the level.
    /// </summary>
    public string holderName;

    /// <summary>
    ///     The maximum height of the level, in chunks.
    /// </summary>
    public uint maxHeight;

    /// <summary>
    ///     The maximum width of the level, in chunks.
    /// </summary>
    public uint maxWidth;

    /// <summary>
    ///     The number of rooms to generate in this level.
    /// </summary>
    public uint numRooms;

    /// <summary>
    ///     The length of the path (in rooms) from the spawn point to the stairs.
    /// </summary>
    public uint pathLength;

    /// <summary>
    ///     All of the possible corridor chunks that can be spawned.
    /// </summary>
    public Corridor[] possibleCorridors;

    /// <summary>
    ///     All of the possible room chunks that can be spawned, excluding start and stair rooms.
    /// </summary>
    public Room[] possiblePathRooms;

    public Room[] possibleStairRooms;

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
        _levelLayout = new Chunk[maxWidth, maxHeight];
        Random.InitState(seed);
        LayoutBorderWall(RandomElement(possibleWalls));
        
        InstantiateLevelLayout();
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
        for (var x = 0; x < maxWidth; x++)
        for (var y = 0; y < maxHeight; y++)
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
        for (var x = 0; x < maxWidth; x++)
            _levelLayout[x, maxHeight - 1] = _levelLayout[x, 0] = wall;
        for (var y = 0; y < maxHeight; y++)
            _levelLayout[maxWidth - 1, y] = _levelLayout[0, y] = wall;
    }

    /// <summary>
    ///     Lays out a path from the start room to the stair room of length PATHLENGTH, including
    ///     both start and stair rooms.
    /// </summary>
    private void LayoutMainPath()
    {
        _levelLayout[Random.Range(0, (int) maxWidth - 1), Random.Range(0, (int) maxHeight - 1)] =
            RandomElement(possibleStartRooms);
        // TODO
    }

    /// <summary>
    ///     Finds the chunk adjacent to the chunk at _levelLayout[x, y] in direction DIR.
    /// </summary>
    /// <param name="x">The x coordinate of the chunk to start from.</param>
    /// <param name="y">The y coordinate of the chunk to start from.</param>
    /// <param name="dir">The direction in which to search for an adjacent chunk.</param>
    /// <returns>
    ///     The chunk adjacent to _levelLayout[x, y] in direction DIR if such a chunk exists.
    ///     null otherwise.
    /// </returns>
    private Chunk AdjacentTo(int x, int y, Direction dir)
    {
        return null; // TODO
    }

    /// <summary>
    ///     Throws exceptions of public variables are set to invalid values. This system of error
    ///     checking seems a bit bad, is there a way to restrict one variable to another in the
    ///     editor?
    /// </summary>
    private void ValidateVariables()
    {
        if (numRooms > (maxHeight - 2) * (maxWidth - 2))
            throw new RuntimeException("Number of rooms exceed maximum.");

        if (pathLength > numRooms || pathLength < 2)
            throw new RuntimeException("Invalid path length.");
    }

    /// TODO: might not need this
    /// <summary>
    ///     Is (x, y) on the border of the level?
    /// </summary>
    /// <param name="x">The horizontal position of the point.</param>
    /// <param name="y">The vertical position of the point.</param>
    /// <returns>Whether x, y lies on the border of the level.</returns>
    private bool OnBorder(int x, int y)
    {
        return x == 0 || y == 0 || x == maxWidth - 1 || y == maxHeight - 1;
    }
}