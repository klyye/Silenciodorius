using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private PlayerController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}