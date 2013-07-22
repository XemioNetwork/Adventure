using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

namespace Xemio.Adventure.Worlds.Entities.Events.Movement
{
    public interface IMovementEvent : IEvent
    {
        /// <summary>
        /// Gets the entity.
        /// </summary>
        Entity Entity { get; }
        /// <summary>
        /// Gets the position.
        /// </summary>
        Vector2 Position { get; }
        /// <summary>
        /// Gets the direction.
        /// </summary>
        Direction Direction { get; }
    }
}
