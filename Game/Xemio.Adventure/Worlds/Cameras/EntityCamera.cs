using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;

namespace Xemio.Adventure.Worlds.Cameras
{
    public class EntityCamera : ICamera
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCamera"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public EntityCamera(Entity entity)
        {
            this.Entity = entity;
            this.InterpolationSpeed = 0.035f;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        /// <summary>
        /// Gets or sets the interpolation speed.
        /// </summary>
        public float InterpolationSpeed { get; set; }
        #endregion
        
        #region Implementation of ICamera
        /// <summary>
        /// Gets the position.
        /// </summary>
        public Vector2 Position { get; private set; }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            this.Position += (this.Entity.Position - this.Position) * this.InterpolationSpeed;
        }
        #endregion
    }
}
