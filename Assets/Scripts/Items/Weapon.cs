using UnityEngine;

namespace Items
{
    /// <summary>
    ///     Any Item that can be equipped to attack.
    /// </summary>
    public abstract class Weapon : Item
    {
        /// <summary>
        ///     Issues an attack at the target.
        /// </summary>
        /// <param name="target">The location that is being attacked.</param>
        /// <param name="isEnemy">Is the attacker an enemy?</param>
        public abstract void Attack(Vector2 target, bool isEnemy);
    }
}