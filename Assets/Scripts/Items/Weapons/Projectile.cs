using System;
using Units;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Items.Weapons
{
    /// <summary>
    ///     A projectile to be fired by a weapon.
    /// </summary>
    /// 
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(DiesOnNextLevel))]
    public class Projectile : MonoBehaviour
    {
        /// <summary>
        ///     How fast the projectile moves.
        /// </summary>
        public float speed;

        /// <summary>
        ///     The amount of damage that one hit of this projectile does.
        /// </summary>
        public float damage;

        /// <summary>
        ///     The rigidbody 2d component attached to this projectile.
        /// </summary>
        private Rigidbody2D _rigidbody;
        
        /// <summary>
        ///     Did this projectile come from an enemy?
        /// </summary>
        [HideInInspector]
        public bool isEnemy;

        private void FixedUpdate()
        {
            _rigidbody.velocity = speed * transform.right;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var unit = other.GetComponent<Unit>();
            if (unit && unit.isEnemy != isEnemy)
            {
                unit.TakeDamage(damage);
                Destroy(gameObject);
            }

            //TODO: DRY
            var tilemap = other.GetComponent<Tilemap>();
            if (tilemap)
            {
                Destroy(gameObject);
            }
        }
    }
}