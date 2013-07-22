using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;

namespace Xemio.Adventure.Worlds.Entities.Components
{
    public class CollisionComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="boundingBox">The bounding box.</param>
        public CollisionComponent(Entity entity, BoundingBox boundingBox)
            : this(entity, boundingBox, true)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="boundingBox">The bounding box.</param>
        /// <param name="collidable">if set to <c>true</c> [collidable].</param>
        public CollisionComponent(Entity entity, BoundingBox boundingBox, bool collidable) : base(entity)
        {
            this.Collidable = collidable;
            this.BoundingBox = boundingBox;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CollisionComponent"/> is collidable.
        /// </summary>
        public bool Collidable { get; set; }
        /// <summary>
        /// Gets the bounding box.
        /// </summary>
        public BoundingBox BoundingBox { get; private set; }
        #endregion
    }
}
