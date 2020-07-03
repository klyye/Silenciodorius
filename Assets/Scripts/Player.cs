using System;
using UnityEngine;

/// <summary>
///     Handles aspects of the Player that do NOT correspond to player input.
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class Player : Unit
{

    /// <summary>
    ///     TODO DELETE THIS
    /// </summary>
    public bool debugMode;

    /// <summary>
    ///     AHH YES 5Head
    /// </summary>
    private uint _intelligence;

    /// <summary>
    ///     Event that fires when the player reaches the stairs.
    /// </summary>
    public event Action OnStairReached;
    
    private void Update()
    {
        if (debugMode && Input.GetKeyDown(KeyCode.Space))
        {
            OnStairReached?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnStairReached?.Invoke();
    }
}