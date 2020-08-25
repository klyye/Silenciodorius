using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
///     Loads the game scene when the button is pressed.
/// </summary>
[RequireComponent(typeof(Button))]
public class PlayButton : MonoBehaviour
{
    /// <summary>
    ///     Loads the game scene when the start button is pressed.
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
}
