using System;
using UnityEngine;

/// <summary>
///     Anything with this component will die when the player reaches the stairs.
/// </summary>
public class DiesOnNextLevel : MonoBehaviour
{
    /// <summary>
    ///     Kills the gameObject.
    /// </summary>
    private Action Die;

    private void Start()
    {
        Die = () => Destroy(gameObject);
        GameManager.player.OnStairReached += Die;
    }

    private void OnDestroy()
    {
        if (GameManager.player != null) GameManager.player.OnStairReached -= Die;
    }
}