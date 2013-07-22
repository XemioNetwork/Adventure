using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.Adventure.Worlds.Entities
{
    [Flags]
    public enum Direction
    {
        None = 0,

        Left = 1,
        Right = 2,
        Up = 4,
        Down = 8,

        LeftUp = Left | Up,
        UpLeft = Left | Up,

        DownLeft = Left | Down,
        LeftDown = Left | Down,

        RightDown = Right | Down,
        DownRight = Right | Down,

        UpRight = Right | Up,
        RightUp = Right | Up
    }

    public static class DirectionExtensions
    {
        /// <summary>
        /// Converts the specified direction into a vector.
        /// </summary>
        public static Vector2 ToVector2(this Direction direction)
        {
            Vector2 vector = Vector2.Zero;

            if (direction.HasFlag(Direction.Left))
                vector += new Vector2(-1, 0);

            if (direction.HasFlag(Direction.Right))
                vector += new Vector2(1, 0);

            if (direction.HasFlag(Direction.Up))
                vector += new Vector2(0, -1);

            if (direction.HasFlag(Direction.Down))
                vector += new Vector2(0, 1);

            return Vector2.Normalize(vector);
        }
    }
}
