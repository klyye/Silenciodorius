using System;
using Items;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Units
{
    /// <summary>
    ///     Handles aspects of the Player that do NOT correspond to player input.
    /// </summary>
    [RequireComponent(typeof(PlayerController))]
    public class Player : Unit
    {
    
        /// <summary>
        ///     TODO DELETE THIS
        /// </summary>
        public bool debugMode;

        /// <summary>
        ///     AHH YES 5Head
        /// </summary>
        private uint _intelligence;

        /// <summary>
        ///     Event that fires when the player reaches the stairs.
        /// </summary>
        public event Action OnStairReached;

        /// <summary>
        ///     Event that fires when the player dies.
        /// </summary>
        public event Action OnPlayerDeath;

        protected override void Update()
        {
            base.Update();
            #if UNITY_EDITOR
                if (debugMode && Input.GetKeyDown(KeyCode.Space))
                {
                    OnStairReached?.Invoke();
                }
            #endif

            if (Input.GetMouseButtonDown(0) && _attackTimer <= 0 && GameManager.isPlaying)
            {
                var mousePos = Input.mousePosition;
                mousePos.z = GameManager.cam.nearClipPlane;
                var mouseWorldPos = GameManager.cam.ScreenToWorldPoint(mousePos);
                MainhandAttack(mouseWorldPos);
                _attackTimer = attackTime;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Stair"))
                OnStairReached?.Invoke();
        }

        protected override void Die()
        {
            OnPlayerDeath?.Invoke();
            base.Die();
        }
    }
}