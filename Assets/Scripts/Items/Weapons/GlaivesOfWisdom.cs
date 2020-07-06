﻿using UnityEngine;

namespace Items.Weapons
{
    /// <summary>
    ///     Silenciodorius's signature default weapon.
    ///
    ///     From the Dota 2 Wiki:
    ///     Silencer enchants his glaives with his wisdom, dealing additional damage based on his
    ///     Intelligence. Silencer temporarily steals his target's intelligence with each hit.
    /// </summary>
    public class GlaivesOfWisdom : Weapon
    {
        public Projectile glaive;

        private void Start()
        {
            _name = "Glaives of Wisdom"; //is this a magic constant?
        }

        public override void Attack(Vector2 target)
        {
            var proj = Instantiate(glaive, transform.position, Quaternion.identity).transform;
            proj.right = target - (Vector2) proj.position;
        }
    }
}