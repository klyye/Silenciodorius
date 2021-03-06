﻿using UnityEngine;

namespace LevelGeneration
{
    /// <summary>
    ///     Represents the layout of one level.
    /// </summary>
    public class Level
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly Chunk[,] levelLayout;

        /// <summary>
        ///     The x and y coordinates such that levelLayout[x, y] is where the player spawns.
        /// </summary>
        public readonly Vector2Int spawnPoint;

        public Level(Chunk[,] layout, Vector2Int spawn)
        {
            levelLayout = layout;
            spawnPoint = spawn;
        }
    }
}