using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private PlayerController _controller;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
    }
}