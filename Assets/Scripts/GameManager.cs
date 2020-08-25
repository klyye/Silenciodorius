using System;
using UI;
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
    ///     death screen, not in inventory screen).
    /// </summary>
    public static bool isPlaying;

    /// <summary>
    ///     The UI Manager object.
    /// </summary>
    public static UIManager uiManager;
    
    private void Awake()
    {
        cam = Camera.main;
        player = FindObjectOfType<Player>();
        player.OnPlayerDeath += GameOver;
        uiManager = FindObjectOfType<UIManager>();
        QualitySettings.vSyncCount = 1;    //screen tearing is really bad
        Resume();
    }

    /// <summary>
    ///     Loads the Main Menu scene.
    /// </summary>
    private void GameOver()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    /// <summary>
    ///     Pauses the game.
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
        isPlaying = false;
    }

    /// <summary>
    ///     Resumes the game, if paused.
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
        isPlaying = true;
        uiManager.PauseUI = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPlaying)
            {
                Pause();
                uiManager.PauseUI = true;
            }
            else
            {
                Resume();
            }
        }
    }
}