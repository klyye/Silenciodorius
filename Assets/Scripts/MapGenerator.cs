using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    /// <summary>
    /// The maximum height of the map.
    /// </summary>
    public int maxHeight;

    /// <summary>
    /// The maximum width of the map.
    /// </summary>
    public int maxWidth;

    /// <summary>
    /// The seed for the pseudorandom number generator that determines the randomness for this map.
    /// </summary>
    public int seed;

    /// <summary>
    /// The prefab for an impassable wall.
    /// </summary>
    public GameObject wallPrefab;

    /// <summary>
    /// Instantiates the necessary tiles in a maxHeight by maxWidth grid in a layout specified by
    /// _mapLayout. This will act as the randomized dungeon level.
    /// </summary>
    public void GenerateMap()
    {
    }

    /// <summary>
    /// Represents the different possible states that a tile in the map can take.
    /// </summary>
    private enum Terrain
    {
        Wall,
        Ground
    }
}