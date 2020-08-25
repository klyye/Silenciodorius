using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    /// <summary>
    ///     Whether the game is currently being player (i.e. not paused, not in main menu, not in
    ///     death screen).
    /// </summary>
    public bool isPlaying;
    
    private void Awake()
    {
        cam = Camera.main;
        player = FindObjectOfType<Player>();
        player.OnPlayerDeath += GameOver;
    }

    /// <summary>
    ///     Loads the Main Menu scene.
    /// </summary>
    private void GameOver()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}