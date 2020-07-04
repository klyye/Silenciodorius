using System;
using UnityEngine;

namespace Units
{
    /// <summary>
    /// TODO document this
    /// </summary>
    [RequireComponent(typeof(UnitController))]
    public abstract class Unit : MonoBehaviour
    {
        private float _health;
        private float _healthRegen;
        private float _mana;
        private float _manaRegen;
    
        private UnitController _controller;
    
        public event Action OnDeath;

        protected virtual void Start()
        {
            _controller = GetComponent<UnitController>();
        }

        public virtual void TakeDamage(float dmg)
        {
            _health -= dmg;
            if (_health <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}
