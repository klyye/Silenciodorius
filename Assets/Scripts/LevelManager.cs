﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Advances and instantiates the current level.
/// </summary>
[RequireComponent(typeof(LevelGenerator))]
public class LevelManager : MonoBehaviour
{
    
    /// <summary>
    ///     The currently loaded level.
    /// </summary>
    private Level _currLevel;
    
    /// <summary>
    ///     The size to scale each chunk by.
    /// </summary>
    public float chunkSize;
    
    /// <summary>
    ///     The name of the gameObject that parents all of the instantiated prefabs.
    /// </summary>
    private const string HolderName = "Level Holder";

    /// <summary>
    ///     Generates the level!
    /// </summary>
    private LevelGenerator _levelGen;

    private void Start()
    {
    }

    /// <summary>
    ///     Instantiates chunks based on the data in _currLevel.
    /// </summary>
    public void InstantiateCurrentLevel()
    {
        var oldMap = transform.Find(HolderName);
        if (oldMap) DestroyImmediate(oldMap.gameObject);
        var mapHolder = new GameObject(HolderName).transform;
        mapHolder.parent = transform;
        
        _levelGen = GetComponent<LevelGenerator>();
        _currLevel = _levelGen.GenerateLevel();
        for (var x = 0; x < _currLevel.levelLayout.GetLength(0); x++)
        for (var y = 0; y < _currLevel.levelLayout.GetLength(1); y++)
        {
            var spawnPos = Chunk.LENGTH * chunkSize * new Vector2(x, y);
            var chunkPrefab = _currLevel.levelLayout[x, y];
            var spawnedChunk = Instantiate(chunkPrefab, spawnPos, Quaternion.identity).transform;
            spawnedChunk.localScale = chunkSize * Vector2.one;
            spawnedChunk.parent = mapHolder;
        }
    }
}