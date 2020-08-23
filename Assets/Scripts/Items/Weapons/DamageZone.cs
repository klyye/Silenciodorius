using System;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Items.Weapons
{
    /// <summary>
    ///     A zone that deals damage at a set frequency, then despawns.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(DiesOnNextLevel))]
    public class DamageZone : MonoBehaviour
    {
        /// <summary>
        ///     The amount of damage that this zone does.
        /// </summary>
        public float damage;

        /// <summary>
        ///     The number of seconds between damage ticks.
        /// </summary>
        public float timeBtwnTicks;

        /// <summary>
        ///     The number of damage ticks.
        /// </summary>
        public uint numTicks;

        /// <summary>
        ///     Does this DamageZone effect units where isEnemy is false?
        /// </summary>
        [HideInInspector] public bool isEnemy;

        /// <summary>
        ///     The amount of seconds until the next damage tick.
        /// </summary>
        private float _timeUntilTick;

        /// <summary>
        ///     The number of damage ticks remaining until the DamageZone despawns.
        /// </summary>
        private uint _ticksLeft;

        /// <summary>
        ///     A set of all the Units that were damaged during this damage tick.
        /// </summary>
        private ISet<Unit> _unitsInZone;

        /// <summary>
        ///     The sprite renderer for this damage zone.
        /// </summary>
        private SpriteRenderer _spriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            _timeUntilTick = timeBtwnTicks;
            _ticksLeft = numTicks;
            _unitsInZone = new HashSet<Unit>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_ticksLeft <= 0)
            {
                Destroy(gameObject);
                return;
            }

            _timeUntilTick -= Time.deltaTime;
            if (_timeUntilTick <= 0)
            {
                _ticksLeft--;
                Tick();
                _timeUntilTick = timeBtwnTicks;
            }

            _spriteRenderer.color -=
                new Color(0, 0, 0, Time.deltaTime / (numTicks * timeBtwnTicks));
        }

        /// <summary>
        ///     The action taken on each tick. To be more specific, damages all enemies in the zone.
        /// </summary>
        private void Tick()
        {
            foreach (var unit in _unitsInZone)
            {
                if (unit.isEnemy != isEnemy)
                {
                    unit.TakeDamage(damage);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var unit = other.GetComponent<Unit>();
            if (unit)
            {
                _unitsInZone.Add(unit);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var unit = other.GetComponent<Unit>();
            if (unit)
            {
                _unitsInZone.Remove(unit);
            }
        }
    }
}