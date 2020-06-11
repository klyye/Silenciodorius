using Boo.Lang.Runtime;
using UnityEngine;

namespace ExtensionMethods
{
    /// <summary>
    ///     Extension methods for the Direction enum because C# doesn't let me put them directly into the
    ///     enum file for whatever reason.
    /// </summary>
    public static class DirectionExtension
    {
        /// <summary>
        ///     Converts a cardinal direction to a unit vector pointing in its direction.
        /// </summary>
        /// <returns>A unit vector pointing in DIRECTION.</returns>
        public static Vector2 ToVector2(this Direction direction)
        {
            switch (direction)
            {
                case Direction.East:
                    return Vector2.right;
                case Direction.North:
                    return Vector2.up;
                case Direction.South:
                    return Vector2.down;
                case Direction.West:
                    return Vector2.left;
                default:
                    throw new RuntimeException("Invalid direction. You should never see this.");
            }
        }

        /// <summary>
        ///     Gives the opposite direction to this direction.
        /// </summary>
        /// <param name="direction">The direction to find the opposite of.</param>
        /// <returns>The opposite direction.</returns>
        public static Direction Opposite(this Direction direction)
        {
            switch (direction)
            {
                case Direction.East:
                    return Direction.West;
                case Direction.North:
                    return Direction.South;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
                default:
                    throw new RuntimeException("Invalid direction. You should never see this.");
            }
        }
    }
}