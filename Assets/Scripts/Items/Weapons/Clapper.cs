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
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Attack(Vector2 target, bool isEnemy)
        {
            print("Clapped!");
        }
    }
}