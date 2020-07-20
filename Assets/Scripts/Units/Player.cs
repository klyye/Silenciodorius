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
        ///     The first camera in the scene tagged with MainCamera.
        /// TODO: i hate this. why do i need this in the class
        /// </summary>
        private Camera _mainCam;

        /// <summary>
        ///     Event that fires when the player reaches the stairs.
        /// </summary>
        public event Action OnStairReached;
    
        private void Update()
        {
            if (debugMode && Input.GetKeyDown(KeyCode.Space))
            {
                OnStairReached?.Invoke();
            }

            if (Input.GetMouseButtonUp(0))
            {
                var mousePos = Input.mousePosition;
                mousePos.z = _mainCam.nearClipPlane;
                var mouseWorldPos = _mainCam.ScreenToWorldPoint(mousePos);
                _mainhand.Attack(mouseWorldPos, isEnemy);
            }

        }
        
        protected override void Start()
        {
            base.Start();
            _mainhand = Instantiate(startingWeapon, transform);
            _mainCam = Camera.main;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Stair"))
            {
                OnStairReached?.Invoke();
            }
        }
    }
}