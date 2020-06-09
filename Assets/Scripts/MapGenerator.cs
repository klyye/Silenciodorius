using Boo.Lang.Runtime;
using UnityEngine;
using static Utility;

/// <summary>
///     Generates a 2D array of Chunks that represents each level of the dungeon.
/// </summary>
public class MapGenerator : MonoBehaviour
{
    /// <summary>
    ///     The layout of the map, where _mapLayout[x, y] represents the Chunk type at (x, y).
    /// </summary>
    private Chunk[,] _mapLayout;

    /// <summary>
    ///     The size to scale each chunk by.
    /// </summary>
    public float chunkSize;

    /// <summary>
    ///     The name of the empty GameObject that acts as the parent to the map.
    /// </summary>
    public string holderName;

    /// <summary>
    ///     The maximum height of the map, in chunks.
    /// </summary>
    public uint maxHeight;

    /// <summary>
    ///     The maximum width of the map, in chunks.
    /// </summary>
    public uint maxWidth;

    /// <summary>
    ///     The number of rooms to generate in this map.
    /// </summary>
    public uint numRooms;

    /// <summary>
    ///     The length of the path from the spawn point to the stairs.
    /// </summary>
    public uint pathLength;

    /// <summary>
    ///     All of the possible corridor chunks that can be spawned.
    /// </summary>
    public Corridor[] possibleCorridors;

    /// <summary>
    ///     All of the possible room chunks that can be spawned.
    /// </summary>
    public Room[] possibleRooms;

    /// <summary>
    ///     All of the possible wall chunks that can be spawned.
    /// </summary>
    public Wall[] possibleWalls;

    /// <summary>
    ///     The seed for the pseudorandom number generator that determines the randomness for this map.
    /// </summary>
    public int seed;

    /// <summary>
    ///     Instantiates the necessary tiles in a maxHeight by maxWidth grid in a layout specified by
    ///     _mapLayout. This will act as the randomized dungeon level.
    /// </summary>
    public void GenerateMap()
    {
        var oldMap = transform.Find(holderName);
        if (oldMap) DestroyImmediate(oldMap.gameObject);
        var mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        ValidateVariables();
        _mapLayout = new Chunk[maxWidth, maxHeight];
        Random.InitState(seed);

        var numRoomsRemaining = numRooms;
        for (var x = 0; x < maxWidth; x++)
        for (var y = 0; y < maxHeight; y++)
        {
            var spawnPos = Chunk.LENGTH * chunkSize * new Vector2(x, y);
            Chunk chunkPrefab;
            if (OnBorder(x, y) || numRoomsRemaining <= 0)
            {
                chunkPrefab = RandomElement(possibleWalls);
            }
            else
            {
                chunkPrefab = RandomElement(possibleRooms);
                numRoomsRemaining--;
            }

            var spawnedChunk = Instantiate(chunkPrefab, spawnPos, Quaternion.identity);
            spawnedChunk.transform.parent = mapHolder;
            spawnedChunk.transform.localScale = chunkSize * Vector2.one;
            _mapLayout[x, y] = spawnedChunk;
        }
    }

    /// <summary>
    ///     Throws exceptions of public variables are set to invalid values. This system of error
    ///     checking seems a bit bad, is there a way to restrict one variable to another in the editor?
    /// </summary>
    private void ValidateVariables()
    {
        if (numRooms > (maxHeight - 2) * (maxWidth - 2))
            throw new RuntimeException("Number of rooms exceed maximum.");

        if (pathLength > numRooms)
            throw new RuntimeException("Path length exceeds number of rooms.");
    }

    private void PlaceRooms()
    {
    }

    /// <summary>
    ///     Is (x, y) on the border of the map?
    /// </summary>
    /// <param name="x">The horizontal position of the point.</param>
    /// <param name="y">The vertical position of the point.</param>
    /// <returns>Whether x, y lies on the border of the map.</returns>
    private bool OnBorder(int x, int y)
    {
        return x == 0 || y == 0 || x == maxWidth - 1 || y == maxHeight - 1;
    }
}