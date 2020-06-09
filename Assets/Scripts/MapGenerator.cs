using UnityEngine;

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
    public uint chunkSize;

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
        var oldMap = transform.Find(holderName).gameObject;
        if (oldMap) DestroyImmediate(oldMap);

        var mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        _mapLayout = new Chunk[maxWidth, maxHeight];
        Random.InitState(seed);

        for (var x = 0; x < maxWidth; x++)
        for (var y = 0; y < maxHeight; y++)
        {
            var spawnPos = Chunk.LENGTH * chunkSize * new Vector2(x, y);
            var chunkToSpawn = Utility.RandomElement(possibleRooms);
            var spawnedObject = Instantiate(chunkToSpawn, spawnPos, Quaternion.identity);
            spawnedObject.transform.parent = mapHolder;
            spawnedObject.transform.localScale = chunkSize * Vector2.one;
            _mapLayout[x, y] = spawnedObject;
        }
    }
}