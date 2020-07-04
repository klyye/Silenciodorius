using System.Collections.Generic;

namespace Inventory
{
    public class Inventory
    {
        /// <summary>
        ///     Maps items in the inventory to the amount of items the unit holds.
        /// </summary>
        private IDictionary<Item, int> _items = new Dictionary<Item, int>();
    }
}
