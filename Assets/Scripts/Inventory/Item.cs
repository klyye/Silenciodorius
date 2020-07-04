using UnityEngine;

namespace Inventory
{
    public class Item : MonoBehaviour
    {
        /// <summary>
        ///     What this item is called. Each type of Item has a unique name.
        /// </summary>
        private string _name;

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return other != null && other is Item && ((Item)other)._name.Equals(_name);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}