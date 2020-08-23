using Units;
using UnityEngine;

/// <summary>
///     "Please avoid using any sort of 'Find()' method to get references to other objects in
///     your scene. This approach is super popular among Unity users (blame the early learning
///     resources for that), and also super not safe, elegant or efficient. Instead, come up with a
///     plan to link all of your objects together through managers instantiating entities and keeping
///     references to all of them. For instance, you can have a GameManager that instantiates the
///     Player and Enemies at the start of the game, and keeps references to all of them at the moment
///     of instantiation. Later, if an Enemy needs a reference to the Player, it calls its GameManager
///     (which it kept a reference to when it got instantiated), and asks the GameManager for the
///     Player's reference."
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    ///     The instance of the Main Camera.
    /// </summary>
    public static Camera cam;

    /// <summary>
    ///     The instance of the player.
    /// </summary>
    public static Player player;

    private void Awake()
    {
        cam = Camera.main;
        player = FindObjectOfType<Player>();
    }
}