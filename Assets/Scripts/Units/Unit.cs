using System;
using System.Collections.Generic;
using Items;
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

        protected virtual void Start()
        {
            _controller = GetComponent<UnitController>();
        }
    }
}