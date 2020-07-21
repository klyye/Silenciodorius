using System;
using System.Collections.Generic;
using Items;
using Items.Weapons;
using UnityEngine;

namespace Units
{
    /// <summary>
    ///     Anything with health that can equip an item. Not really sure how to describe units.
    /// </summary>
    [RequireComponent(typeof(UnitController))]
    public abstract class Unit : MonoBehaviour
    {
        /// <summary>
        ///     The weapon that the player starts with.
        /// </summary>
        public Weapon startingWeapon;
        
        /// <summary>
        ///     How much health this unit currently has.
        /// </summary>
        protected float _health;

        /// <summary>
        ///     The object that handles the movement of the Unit.
        /// </summary>
        protected UnitController _controller;
        
        /// <summary>
        ///     The current main hand weapon of the Unit.
        /// </summary>
        protected Weapon _mainhand;
        
        /// <summary>
        ///     A collection of items that the unit currently holds. The dictionary maps the Item to
        ///     the amount of that Item which is currently being held.
        /// </summary>
        protected IDictionary<Item, int> _inventory = new Dictionary<Item, int>();

        /// <summary>
        ///     Kills the unit.
        /// </summary>
        public Action Die;

        /// <summary>
        ///     Is this Unit an enemy or an ally? Players can only damage enemies.
        /// </summary>
        public bool isEnemy;

        /// <summary>
        ///     How much health this unit starts with.
        /// </summary>
        public float startingHealth;

        /// <summary>
        ///     How many seconds must pass after an attack before a unit can attack again.
        /// </summary>
        public float attackTime;

        /// <summary>
        ///     The time, in seconds, until the next attack.
        /// </summary>
        protected float _attackTimer;

        protected virtual void Start()
        {
            _controller = GetComponent<UnitController>();
            Die = () => Destroy(gameObject);
            _health = startingHealth;
            _attackTimer = attackTime;
        }

        /// <summary>
        ///     Deals DMG damage to the unit.
        /// </summary>
        /// <param name="dmg">Damage to deal to the unit.</param>
        public void TakeDamage(float dmg)
        {
            _health -= dmg;
            print($"Ow! {gameObject.name} just took {dmg} damage!");
            if (_health <= 0)
            {
                Die();
            }
        }
    }
}