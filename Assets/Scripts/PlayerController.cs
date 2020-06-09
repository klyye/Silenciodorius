using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles aspects of the player that correspond to player input.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
  
    /// <summary>
    /// How fast the player moves.
    /// </summary>
    public float speed;

    /// <summary>
    /// The rigidbody2d component attached to the player.
    /// </summary>
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _rigidbody.velocity = speed * input;
    }
}