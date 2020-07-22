using UnityEngine;

namespace Units
{
    /// <summary>
    ///     Handles any movement of the Unit.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class UnitController : MonoBehaviour
    {
        public uint movespeed;

        /// <summary>
        ///     The rigidbody2d component attached to the unit.
        /// </summary>
        private Rigidbody2D _rigidbody;

        protected virtual void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public Vector2 Direction
        {
            set => _rigidbody.velocity = movespeed * value.normalized;
        }
    }
}