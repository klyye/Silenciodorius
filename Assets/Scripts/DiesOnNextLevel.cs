using System;
using System.Collections;
using System.Collections.Generic;
using Units;
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
        var player = GameManager.player;
        if (player != null) player.OnStairReached -= Die;
    }
}
