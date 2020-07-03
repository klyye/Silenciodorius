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
    private float _healthRegen;
    private float _mana;
    private float _manaRegen;
    private uint _level;
    private uint _exp;
    
    private UnitController _controller;

    private void Start()
    {
        _controller = GetComponent<UnitController>();
    }
}
