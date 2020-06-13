using System;
using UnityEngine;

/// <summary>
///     Handles aspects of the Player that do NOT correspond to player input.
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    /// <summary>
    ///     The controller associated with the player.
    /// </summary>
    private PlayerController _controller;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Triggered!");
        print(other.gameObject.tag);
    }
}