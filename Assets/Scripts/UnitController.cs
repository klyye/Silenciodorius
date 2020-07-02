using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO: document this
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class UnitController : MonoBehaviour
{
    private uint _movespeed;
    
    /// <summary>
    ///     The rigidbody2d component attached to the player.
    /// </summary>
    private Rigidbody2D _rigidbody;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Move(Vector2 dir)
    {
        _rigidbody.velocity = _movespeed * dir.normalized;
    }
}
