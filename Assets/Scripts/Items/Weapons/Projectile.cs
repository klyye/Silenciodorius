﻿using System;
using Units;
using UnityEngine;

namespace Items.Weapons
{
    /// <summary>
    ///     A projectile to be fired by a weapon.
    /// </summary>
    /// 
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        /// <summary>
        ///     Kills the projectile.
        /// </summary>
        private Action Die;

        /// <summary>
        ///     How fast the projectile moves.
        /// </summary>
        public float speed;

        /// <summary>
        ///     The amount of damage that one hit of this projectile does.
        /// </summary>
        public float damage;

        private void FixedUpdate()
        {
            transform.Translate(Time.fixedDeltaTime * speed * transform.right, Space.World);
        }

        private void Start()
        {
            Die = () => Destroy(gameObject);
            FindObjectOfType<Player>().OnStairReached += Die;
        }

        private void OnDestroy()
        {
            var player = FindObjectOfType<Player>();
            if (player != null) player.OnStairReached -= Die;
        }
    }
}