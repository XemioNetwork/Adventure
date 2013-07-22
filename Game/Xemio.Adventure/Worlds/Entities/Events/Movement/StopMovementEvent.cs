using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;

namespace Xemio.Adventure.Worlds.Entities.Events.Movement
{
    public class StopMovementEvent : IMovementEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StartMovementEvent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="position">The position.</param>
        /// <param name="direction">The direction.</param>
        public StopMovementEvent(Entity entity, Vector2 position, Direction direction)
        {
            this.Entity = entity;
            this.Position = position;
            this.Direction = direction;
        }
        #endregion

        #region Implementation of IMovementEvent
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        /// <summary>
        /// Gets the position.
        /// </summary>
        public Vector2 Position { get; private set; }
        /// <summary>
        /// Gets the direction.
        /// </summary>
        public Direction Direction { get; private set; }
        #endregion
    }
}
