using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds.Entities.Components;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Sprites;

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
            this.InterpolationSpeed = 0.05f;
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
            Vector2 animationOffset = Vector2.Zero;
            Vector2 increment = this.Entity.Position - this.Position;

            var animationComponent = this.Entity.GetComponent<AnimationComponent>();
            if (animationComponent != null)
            {
                AnimationInstance current = animationComponent.Current;
                if (current != null)
                {
                    animationOffset = new Vector2(
                        current.Frame.Width * 0.5f,
                        current.Frame.Height * 0.5f);
                }
            }

            this.Position += (increment + animationOffset) * this.InterpolationSpeed;
        }
        #endregion
    }
}
