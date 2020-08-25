using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
///     Loads the main menu when pressed.
/// </summary>
[RequireComponent(typeof(Button))]
public class MainMenuButton : MonoBehaviour
{
    /// <summary>
    ///     Loads the Main Menu scene.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
