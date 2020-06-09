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
    ///     TODO: temporary variable, delete this!
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
    ///     All of the possible chunks types that can be spawned.
    /// </summary>
    public Chunk[] possibleChunks;

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
            var spawnPos = chunkSize * new Vector2(x, y);
            var chunkToSpawn = Utility.RandomElement(possibleChunks);
            print("Chunk to spawn: " + chunkToSpawn);
            var spawnedObject = Instantiate(chunkToSpawn, spawnPos, Quaternion.identity);
            spawnedObject.transform.parent = mapHolder;
            _mapLayout[x, y] = spawnedObject;
        }
    }
}