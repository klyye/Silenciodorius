using UnityEngine;

namespace Units
{
    /// <summary>
    ///     Handles aspects of the player that correspond to player input.
    /// </summary>
    public class PlayerController : UnitController
    {

        private void FixedUpdate()
        {
            var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Move(input);
        }
    }
}