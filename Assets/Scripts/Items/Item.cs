using UnityEngine;

namespace Items
{
    /// <summary>
    ///     Anything that can go into the player's inventory.
    /// </summary>
    public abstract class Item : MonoBehaviour
    {
        /// <summary>
        ///     What this item is called. Each type of Item has a unique name.
        /// </summary>
        protected string _name;

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return other != null && other is Item item && item._name.Equals(_name);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}