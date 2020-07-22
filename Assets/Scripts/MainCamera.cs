using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     This script contains additional logic and caching for the main camera of the scene.
/// </summary>
[RequireComponent(typeof(Camera))]
public class MainCamera : MonoBehaviour
{
    /// <summary>
    ///     The instance of the Main Camera.
    /// </summary>
    public static Camera instance;

    private void Awake()
    {
        instance = Camera.main;
    }
}
