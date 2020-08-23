using System;
using System.Collections.Generic;
using Items;
using Items.Weapons;
using UI;
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
        private Weapon _mainhand;
        
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
        ///     The health bar prefab of this unit.
        /// </summary>
        public HealthBar healthBarPrefab;

        /// <summary>
        ///     The health bar instance currently keeping track of this unit's health.
        ///     TODO: is there a better way to do this?
        /// </summary>
        private HealthBar _currHealthBar;
        
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
            _mainhand = Instantiate(startingWeapon, transform);
            _currHealthBar = Instantiate(healthBarPrefab, transform);
            _currHealthBar.SetHealth(startingHealth, startingHealth);
        }

        /// <summary>
        ///     If the attack timer allows it, attack the target using the mainhand weapon.
        /// </summary>
        /// <param name="target">The location to attack with the weapon.</param>
        protected void MainhandAttack(Vector2 target)
        {
            if (_attackTimer <= 0)
            {
                _attackTimer = attackTime;
                _mainhand.Attack(target, isEnemy);
            }
        }

        protected virtual void Update()
        {
            _attackTimer -= Time.deltaTime;
        }

        /// <summary>
        ///     Deals DMG damage to the unit.
        /// </summary>
        /// <param name="dmg">Damage to deal to the unit.</param>
        public void TakeDamage(float dmg)
        {
            _health -= dmg;
            _currHealthBar.SetHealth(_health, startingHealth);
            if (_health <= 0) Die();
        }
    }
}