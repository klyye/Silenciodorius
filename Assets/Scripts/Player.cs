using System;
using UnityEngine;

/// <summary>
///     Handles aspects of the Player that do NOT correspond to player input.
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class Player : Unit
{
    /// <summary>
    ///     The controller associated with the player.
    /// </summary>
    private PlayerController _controller;

    /// <summary>
    ///     TODO DELETE THIS
    /// </summary>
    public bool debugMode;

    public event Action OnStairReached;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

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