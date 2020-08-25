using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///     Controls the Main Menu UI elements in the Main Menu scene.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    ///     Loads the game scene when the start button is pressed.
    /// </summary>
    public void StartButton()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
}
