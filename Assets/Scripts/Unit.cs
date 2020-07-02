using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO document this
/// </summary>
[RequireComponent(typeof(UnitController))]
public abstract class Unit : MonoBehaviour
{
    private float _health;
    private uint _strength;
    private uint _agility;
    private uint _intelligence;
    private UnitController _controller;

    private void Start()
    {
        _controller = GetComponent<UnitController>();
    }
}
