using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Weapons
{
    /// <summary>
    ///     A weapon that executes a "clap" attack, dealing damage in an AoE.
    /// </summary>
    public class Clapper : Weapon
    {
        /// <summary>
        ///     The DamageZone that is spawned when the clapper attacks.
        /// </summary>
        public DamageZone damageZone;

        public override void Attack(Vector2 target, bool isEnemy)
        {
            var zone = Instantiate(damageZone, target, Quaternion.identity);
            zone.isEnemy = isEnemy;
        }
    }
}